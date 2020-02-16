using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Circular;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles.Circular;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles.Circular;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.Beam.Circular
{
    /// <summary>
    /// It's responsible to calculate the vibration in a circular beam.
    /// </summary>
    public class CalculateCircularBeamVibration : CalculateBeamVibration<CircularProfile>, ICalculateCircularBeamVibration
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
        /// <param name="arrayOperation"></param>
        public CalculateCircularBeamVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            ICircularProfileValidator profileValidator,
            ICircularProfileMapper profileMapper,
            IAuxiliarOperation auxiliarOperation,
            ICircularBeamMainMatrix mainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, profileMapper, auxiliarOperation, mainMatrix, arrayOperation)
        {
        }
    }
}
