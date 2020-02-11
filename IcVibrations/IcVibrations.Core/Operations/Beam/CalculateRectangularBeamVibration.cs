using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.Beam
{
    public class CalculateRectangularBeamVibration : CalculateBeamVibration<RectangularProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="profileBuilder"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateRectangularBeamVibration(
            INewmarkMethod<Beam<RectangularProfile>, RectangularProfile> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<RectangularProfile> profileValidator, 
            IProfileMapper<RectangularProfile> profileBuilder, 
            IAuxiliarOperation auxiliarOperation) 
            : base(newmarkMethod, mappingResolver, profileValidator, profileBuilder, auxiliarOperation)
        {
        }
    }
}
