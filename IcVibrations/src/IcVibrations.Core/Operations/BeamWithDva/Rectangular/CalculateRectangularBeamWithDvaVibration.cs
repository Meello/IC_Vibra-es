using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Rectangular;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles.Rectangular;
using IcVibrations.Core.NewmarkNumericalIntegration.BeamWithDva;
using IcVibrations.Core.Validators.Profiles.Rectangular;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.BeamWithDva.Rectangular
{
    /// <summary>
    /// It's responsible to calculate the vibration in a rectangular beam with dynamic vibration absorber.
    /// </summary>
    public class CalculateRectangularBeamWithDvaVibration : CalculateBeamWithDvaVibration<RectangularProfile>, ICalculateRectangularBeamWithDvaVibration
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="beamMainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public CalculateRectangularBeamWithDvaVibration(
            IBeamWithDvaNewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IRectangularProfileValidator profileValidator,
            IAuxiliarOperation auxiliarOperation,
            IRectangularProfileMapper profileMapper,
            IBeamWithDvaMainMatrix mainMatrix,
            IRectangularBeamMainMatrix beamMainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper, mainMatrix, beamMainMatrix, arrayOperation)
        {
        }
    }
}
