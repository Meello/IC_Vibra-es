using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;

namespace IcVibrations.Core.Operations.Beam
{
    public class CalculateCircularBeamVibration : CalculateBeamVibration<CircularProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="profileBuilder"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateCircularBeamVibration(
            INewmarkMethod<Models.Beam.Beam<CircularProfile>, CircularProfile> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<CircularProfile> profileValidator, 
            IProfileMapper<CircularProfile> profileBuilder, 
            IAuxiliarOperation auxiliarOperation) 
            : base(newmarkMethod, mappingResolver, profileValidator, profileBuilder, auxiliarOperation)
        {
        }
    }
}
