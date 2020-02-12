using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.CalculateVibration.BeamWithPiezoelectric;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with piezoelectric.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamWithPiezoelectricVibration<TProfile> : CalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<TProfile>, PiezoelectricRequestData<TProfile>, TProfile, BeamWithPiezoelectric<TProfile>>
        where TProfile : Profile, new()
    {
        private readonly IMappingResolver _mappingResolver;
        private readonly IAuxiliarOperation _auxiliarOperation;
        private readonly IProfileMapper<TProfile> _profileMapper;
        private readonly IBeamWithPiezoelectricMainMatrix<TProfile> _mainMatrix;
        private readonly ICommonMainMatrix _commonMainMatrix;
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="commonMainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public CalculateBeamWithPiezoelectricVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<TProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<TProfile> profileMapper,
            IBeamWithPiezoelectricMainMatrix<TProfile> mainMatrix,
            ICommonMainMatrix commonMainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._auxiliarOperation = auxiliarOperation;
            this._profileMapper = profileMapper;
            this._mainMatrix = mainMatrix;
            this._commonMainMatrix = commonMainMatrix;
            this._arrayOperation = arrayOperation;
        }

        public async override Task<BeamWithPiezoelectric<TProfile>> BuildBeam(CalculateBeamWithPiezoelectricVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            return new BeamWithPiezoelectric<TProfile>()
            {
                DielectricConstant = request.BeamData.DielectricConstant,
                DielectricPermissiveness = request.BeamData.DielectricPermissiveness,
                ElasticityConstant = request.BeamData.ElasticityConstant,
                ElementsWithPiezoelectric = request.BeamData.ElementsWithPiezoelectric,
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                PiezoelectricConstant = request.BeamData.PiezoelectricConstant,
                PiezoelectricProfile = request.BeamData.PiezoelectricProfile,
                Profile = request.BeamData.Profile,
                PiezoelectricSpecificMass = request.BeamData.PiezoelectricSpecificMass,
                YoungModulus = request.BeamData.PiezoelectricYoungModulus,
                ElectricalCharge = new double[degreesFreedomMaximum]
            };
        }

        public async override Task<NewmarkMethodInput> CreateInput(BeamWithPiezoelectric<TProfile> beam, NewmarkMethodParameter newmarkMethodParameter, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            uint piezoelectricDegreesFreedomMaximum = beam.NumberOfElements;

            bool[] bondaryCondition = await this._commonMainMatrix.CalculateBeamBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);
            uint numberOfTrueBoundaryConditions = 0;

            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    numberOfTrueBoundaryConditions += 1;
                }
            }

            // Main matrixes to create input.
            double[,] mass = await this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = await this._mainMatrix.CalculateHardness(beam, degreesFreedomMaximum);

            double[,] piezoelectricElectromechanicalCoupling = await this._mainMatrix.CalculatePiezoelectricElectromechanicalCoupling(beam, degreesFreedomMaximum);

            double[,] piezoelectricCapacitance = await this._mainMatrix.CalculatePiezoelectricCapacitance(beam);

            double[,] equivalentMass = await this._mainMatrix.CalculateEquivalentMass(mass, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);

            double[,] equivalentHardness = await this._mainMatrix.CalculateEquivalentHardness(hardness, piezoelectricElectromechanicalCoupling, piezoelectricCapacitance, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);
            
            double[,] damping = await this._mainMatrix.CalculateDamping(equivalentMass, equivalentHardness, degreesFreedomMaximum);

            double[] force = beam.Forces;

            double[] electricalCharge = beam.ElectricalCharge;

            double[] equivalentForce = await this._arrayOperation.MergeArray(force, electricalCharge);

            // Creating input.
            input.Mass = this._auxiliarOperation.ApplyBondaryConditions(equivalentMass, bondaryCondition, numberOfTrueBoundaryConditions + piezoelectricDegreesFreedomMaximum);

            input.Hardness = this._auxiliarOperation.ApplyBondaryConditions(equivalentHardness, bondaryCondition, numberOfTrueBoundaryConditions + piezoelectricDegreesFreedomMaximum);

            input.Damping = this._auxiliarOperation.ApplyBondaryConditions(damping, bondaryCondition, numberOfTrueBoundaryConditions + piezoelectricDegreesFreedomMaximum);

            input.Force = this._auxiliarOperation.ApplyBondaryConditions(equivalentForce, bondaryCondition, numberOfTrueBoundaryConditions + piezoelectricDegreesFreedomMaximum);

            input.NumberOfTrueBoundaryConditions = numberOfTrueBoundaryConditions;

            input.Parameter = newmarkMethodParameter;

            return input;
        }
    }
}
