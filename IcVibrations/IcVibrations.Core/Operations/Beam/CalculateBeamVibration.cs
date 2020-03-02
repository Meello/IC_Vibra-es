using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.CalculateVibration.Beam;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.Beam
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamVibration<TProfile> : CalculateVibration<CalculateBeamVibrationRequest<TProfile>, BeamRequestData<TProfile>, TProfile, Beam<TProfile>>, ICalculateBeamVibration<TProfile>
        where TProfile : Profile, new()
    {
        private readonly IMappingResolver _mappingResolver;
        private readonly IProfileMapper<TProfile> _profileMapper;
        private readonly IAuxiliarOperation _auxiliarOperation;
        private readonly IBeamMainMatrix<TProfile> _mainMatrix;
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="profileMapper"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public CalculateBeamVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<TProfile> profileValidator,
            IProfileMapper<TProfile> profileMapper,
            IAuxiliarOperation auxiliarOperation,
            IBeamMainMatrix<TProfile> mainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._profileMapper = profileMapper;
            this._auxiliarOperation = auxiliarOperation;
            this._mainMatrix = mainMatrix;
            this._arrayOperation = arrayOperation;
        }

        public async override Task<Beam<TProfile>> BuildBeam(CalculateBeamVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            GeometricProperty geometricProperty = new GeometricProperty();

            if(request.BeamData.Profile.Area != default && request.BeamData.Profile.MomentOfInertia != default)
            {
                geometricProperty.Area = await this._arrayOperation.Create(request.BeamData.Profile.Area.Value, request.BeamData.NumberOfElements);
                geometricProperty.MomentOfInertia = await this._arrayOperation.Create(request.BeamData.Profile.MomentOfInertia.Value, request.BeamData.NumberOfElements);
            }
            else
            {
                geometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum);
            }

            return new Beam<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = geometricProperty,
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                Profile = request.BeamData.Profile
            };
        }

        public async override Task<NewmarkMethodInput> CreateInput(Beam<TProfile> beam, NewmarkMethodParameter newmarkMethodParameter, uint degreesFreedomMaximum)
        {
            bool[] bondaryCondition = await this._mainMatrix.CalculateBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);
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

            double[,] damping = await this._mainMatrix.CalculateDamping(mass, hardness);

            double[] forces = beam.Forces;

            // Creating input.
            NewmarkMethodInput input = new NewmarkMethodInput
            {
                Mass = this._auxiliarOperation.ApplyBondaryConditions(mass, bondaryCondition, numberOfTrueBoundaryConditions),

                Hardness = this._auxiliarOperation.ApplyBondaryConditions(hardness, bondaryCondition, numberOfTrueBoundaryConditions),

                Damping = this._auxiliarOperation.ApplyBondaryConditions(damping, bondaryCondition, numberOfTrueBoundaryConditions),

                Force = this._auxiliarOperation.ApplyBondaryConditions(forces, bondaryCondition, numberOfTrueBoundaryConditions),

                NumberOfTrueBoundaryConditions = numberOfTrueBoundaryConditions,

                Parameter = newmarkMethodParameter
            };

            return input;
        }
    }
}