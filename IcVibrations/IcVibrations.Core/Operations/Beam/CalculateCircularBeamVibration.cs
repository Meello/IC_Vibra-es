using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.Beam
{
    /// <summary>
    /// It's responsible to calculate the vibration in a circular beam.
    /// </summary>
    public class CalculateCircularBeamVibration : CalculateBeamVibration<CircularProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="profileMapper"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="commonMainMatrix"></param>
        public CalculateCircularBeamVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver, 
            IProfileValidator<CircularProfile> profileValidator,
            IProfileMapper<CircularProfile> profileMapper, 
            IAuxiliarOperation auxiliarOperation, 
            IBeamMainMatrix<CircularProfile> mainMatrix, 
            ICommonMainMatrix commonMainMatrix) 
            : base(newmarkMethod, mappingResolver, profileValidator, profileMapper, auxiliarOperation, mainMatrix, commonMainMatrix)
        {
        }
    }
}
