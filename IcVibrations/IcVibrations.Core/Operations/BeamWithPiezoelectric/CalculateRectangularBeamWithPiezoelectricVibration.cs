using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with piezoelectric.
    /// </summary>
    public class CalculateRectangularBeamWithPiezoelectricVibration : CalculateBeamWithPiezoelectricVibration<RectangularProfile>
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
        /// <param name="commonMainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public CalculateRectangularBeamWithPiezoelectricVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<RectangularProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation, 
            IProfileMapper<RectangularProfile> profileMapper, 
            IBeamWithPiezoelectricMainMatrix<RectangularProfile> mainMatrix,
            ICommonMainMatrix commonMainMatrix,
            IArrayOperation arrayOperation)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper, mainMatrix, commonMainMatrix, arrayOperation)
        {
        }
    }
}
