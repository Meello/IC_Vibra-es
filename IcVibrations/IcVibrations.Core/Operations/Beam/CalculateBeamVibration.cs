using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.Beam
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamVibration<TProfile> : CalculateVibration<CalculateBeamVibrationRequest<TProfile, BeamRequestData<TProfile>>, BeamRequestData<TProfile>, TProfile, Beam<TProfile>>
        where TProfile : Profile, new()
    {
        private readonly IProfileMapper<TProfile> _profileMapper;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="profileBuilder"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateBeamVibration(
            INewmarkMethod<Beam<TProfile>, TProfile> newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<TProfile> profileValidator,
            IProfileMapper<TProfile> profileMapper,
            IAuxiliarOperation auxiliarOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._profileMapper = profileMapper;
        }

        public async override Task<Beam<TProfile>> BuildBeam(CalculateBeamVibrationRequest<TProfile, BeamRequestData<TProfile>> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            return new Beam<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = default,
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements
            };
        }
    }
}