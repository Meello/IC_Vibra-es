using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the vibration in a circular beam with piezoelectric.
    /// </summary>
    public class CalculateCircularBeamWithPiezoelectric : CalculateBeamWithPiezoelectric<CircularProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        public CalculateCircularBeamWithPiezoelectric(
            INewmarkMethod<BeamWithPiezoelectric<CircularProfile>, CircularProfile> newmarkMethod,
            IMappingResolver mappingResolver, 
            IProfileValidator<CircularProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation, 
            IProfileMapper<CircularProfile> profileMapper) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper)
        {
        }
    }
}
