using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Rectangular;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles.Rectangular;
using IcVibrations.Core.NewmarkNumericalIntegration.Beam;
using IcVibrations.Core.Validators.Profiles.Rectangular;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.Beam.Rectangular
{
    /// <summary>
    /// It's responsible to calculate the vibration in a rectangular beam.
    /// </summary>
    public class CalculateRectangularBeamVibration : CalculateBeamVibration<RectangularProfile>, ICalculateRectangularBeamVibration
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
        public CalculateRectangularBeamVibration(
            IBeamNewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IRectangularProfileValidator profileValidator,
            IRectangularProfileMapper profileMapper,
            IAuxiliarOperation auxiliarOperation,
            IRectangularBeamMainMatrix mainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, profileMapper, auxiliarOperation, mainMatrix, arrayOperation)
        {
        }
    }
}
