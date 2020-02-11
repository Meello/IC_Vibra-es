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
    /// It's responsible to calculate the vibration in a beam with piezoelectric.
    /// </summary>
    public class CalculateRectangularBeamWithPiezoelectric : CalculateBeamWithPiezoelectric<RectangularProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        public CalculateRectangularBeamWithPiezoelectric(
            INewmarkMethod<BeamWithPiezoelectric<RectangularProfile>, RectangularProfile> newmarkMethod,
            IMappingResolver mappingResolver, 
            IProfileValidator<RectangularProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation, 
            IProfileMapper<RectangularProfile> profileMapper) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper)
        {
        }
    }
}
