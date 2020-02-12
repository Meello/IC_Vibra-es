using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO;
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
    public abstract class CalculateBeamVibration<TProfile> : CalculateVibration<CalculateBeamVibrationRequest<TProfile>, BeamRequestData<TProfile>, TProfile, Beam<TProfile>>
        where TProfile : Profile, new()
    {
        private readonly IMappingResolver _mappingResolver;
        private readonly IProfileMapper<TProfile> _profileMapper;
        private readonly ICommonMainMatrix _mainMatrix;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateBeamVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<TProfile> profileValidator,
            IProfileMapper<TProfile> profileMapper,
            IAuxiliarOperation auxiliarOperation,
            ICommonMainMatrix mainMatrix)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._profileMapper = profileMapper;
            this._mainMatrix = mainMatrix;
        }

        public async override Task<Beam<TProfile>> BuildBeam(CalculateBeamVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            return new Beam<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                Profile = request.BeamData.Profile
            };
        }

        public async override Task<NewmarkMethodInput> CreateInput(Beam<TProfile> beam, NewmarkMethodParameter newmarkMethodParameter, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            input.Mass = this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            return input;
        }
    }
}