using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorber.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamWithDvaVibration<TProfile> : CalculateVibration<CalculateBeamWithDvaVibrationRequest<TProfile>, TProfile, BeamWithDva<TProfile>>
        where TProfile : Profile, new()
    {
        private readonly IProfileMapper<TProfile> _profileMapper;

        public CalculateBeamWithDvaVibration(
            INewmarkMethod<BeamWithDva<TProfile>, TProfile> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<TProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation) : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
        }

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateBeamWithDvaVibration(
            INewmarkMethod<BeamWithDva<TProfile>, TProfile> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<TProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<TProfile> profileMapper) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._profileMapper = profileMapper;
        }

        public async override Task<BeamWithDva<TProfile>> BuildBeam(CalculateBeamWithDvaVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            return new BeamWithDva<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = default,
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                Profile = request.BeamData.Profile,
                DvaHardnesses = request.BeamData.Dvas
            };
        }
    }
}
