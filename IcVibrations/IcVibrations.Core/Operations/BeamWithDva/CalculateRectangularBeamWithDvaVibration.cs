using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a rectangular beam with dynamic vibration absorber.
    /// </summary>
    public class CalculateRectangularBeamWithDvaVibration : CalculateBeamWithDvaVibration<RectangularProfile>
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
        /// <param name="commonMainMatrix"></param>
        public CalculateRectangularBeamWithDvaVibration(
            INewmarkMethod newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<RectangularProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<RectangularProfile> profileMapper, 
            IBeamWithDvaMainMatrix mainMatrix, 
            IBeamMainMatrix<RectangularProfile> beamMainMatrix,
            ICommonMainMatrix commonMainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper, mainMatrix, beamMainMatrix, commonMainMatrix, arrayOperation)
        {
        }
    }
}
