using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric.Circular;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles.Circular;
using IcVibrations.Core.NewmarkNumericalIntegration.BeamWithPiezoelectric;
using IcVibrations.Core.Validators.Profiles.Circular;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric.Circular
{
    /// <summary>
    /// It's responsible to calculate the vibration in a circular beam with piezoelectric.
    /// </summary>
    public class CalculateCircularBeamWithPiezoelectricVibration : CalculateBeamWithPiezoelectricVibration<CircularProfile>, ICalculateCircularBeamWithPiezoelectricVibration
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public CalculateCircularBeamWithPiezoelectricVibration(
            IBeamWithPiezoelectricNewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            ICircularProfileValidator profileValidator,
            IAuxiliarOperation auxiliarOperation,
            ICircularProfileMapper profileMapper,
            ICircularBeamWithPiezoelectricMainMatrix mainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper, mainMatrix, arrayOperation)
        {
        }
    }
}
