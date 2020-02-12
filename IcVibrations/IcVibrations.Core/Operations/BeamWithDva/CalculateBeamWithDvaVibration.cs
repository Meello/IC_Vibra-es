using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorber.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamWithDvaVibration<TProfile> : CalculateVibration<CalculateBeamWithDvaVibrationRequest<TProfile>, BeamWithDvaRequestData<TProfile>, TProfile, BeamWithDva<TProfile>>
        where TProfile : Profile, new()
    {
        private readonly IMappingResolver _mappingResolver;
        private readonly IAuxiliarOperation _auxiliarOperation;
        private readonly IProfileMapper<TProfile> _profileMapper;
        private readonly IBeamWithDvaMainMatrix _mainMatrix;
        private readonly IBeamMainMatrix<TProfile> _beamMainMatrix;
        private readonly ICommonMainMatrix _commonMainMatrix;

        /// <summary>
        /// Class contructor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="beamMainMatrix"></param>
        /// <param name="commonMainMatrix"></param>
        public CalculateBeamWithDvaVibration(
            INewmarkMethod newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<TProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<TProfile> profileMapper,
            IBeamWithDvaMainMatrix mainMatrix,
            IBeamMainMatrix<TProfile> beamMainMatrix,
            ICommonMainMatrix commonMainMatrix) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._auxiliarOperation = auxiliarOperation;
            this._profileMapper = profileMapper;
            this._mainMatrix = mainMatrix;
            this._beamMainMatrix = beamMainMatrix;
            this._commonMainMatrix = commonMainMatrix;
        }

        public async override Task<BeamWithDva<TProfile>> BuildBeam(CalculateBeamWithDvaVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            int i = 0;
            
            double[] dvaMasses = new double[request.BeamData.Dvas.Count];
            double[] dvaHardnesses = new double[request.BeamData.Dvas.Count];
            uint[] dvaNodePositions = new uint[request.BeamData.Dvas.Count];

            foreach (DynamicVibrationAbsorber dva in request.BeamData.Dvas)
            {
                dvaMasses[i] = dva.DvaMass;
                dvaHardnesses[i] = dva.DvaHardness;
                dvaNodePositions[i] = dva.DvaNodePosition;
                i += 1;
            }

            return new BeamWithDva<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                Profile = request.BeamData.Profile,
                DvaHardnesses = dvaHardnesses,
                DvaMasses = dvaMasses,
                DvaNodePositions = dvaNodePositions
            };
        }

        public async override Task<NewmarkMethodInput> CreateInput(BeamWithDva<TProfile> beam, NewmarkMethodParameter newmarkMethodParameter, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            double[,] mass = await this._beamMainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = await this._beamMainMatrix.CalculateHardness(beam, degreesFreedomMaximum);

            double[,] massWithDva = await this._mainMatrix.CalculateMassWithDva(mass, beam.DvaMasses, beam.DvaNodePositions);

            double[,] hardnessWithDva = await this._mainMatrix.CalculateHardnessWithDva(hardness, beam.DvaHardnesses, beam.DvaNodePositions);

            double[,] dampingWithDva = await this._mainMatrix.CalculateDamping(massWithDva, hardnessWithDva, degreesFreedomMaximum);

            double[] forces = beam.Forces;

            bool[] bondaryCondition = await this._commonMainMatrix.CalculateBeamBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);
            uint numberOfTrueBoundaryConditions = 0;

            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    numberOfTrueBoundaryConditions += 1;
                }
            }

            input.Mass = this._auxiliarOperation.AplyBondaryConditions(massWithDva, bondaryCondition, numberOfTrueBoundaryConditions);

            input.Hardness = this._auxiliarOperation.AplyBondaryConditions(hardnessWithDva, bondaryCondition, numberOfTrueBoundaryConditions);

            input.Damping = await this._mainMatrix.CalculateDamping(input.Mass, input.Hardness, numberOfTrueBoundaryConditions);

            input.Force = this._auxiliarOperation.AplyBondaryConditions(forces, bondaryCondition, numberOfTrueBoundaryConditions);

            input.NumberOfTrueBoundaryConditions = numberOfTrueBoundaryConditions;

            input.Parameter = newmarkMethodParameter;

            return input;
        }
    }
}
