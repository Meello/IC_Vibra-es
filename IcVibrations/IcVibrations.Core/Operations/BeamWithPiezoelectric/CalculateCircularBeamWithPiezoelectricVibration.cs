using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.Methods.AuxiliarOperations;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the vibration in a circular beam with piezoelectric.
    /// </summary>
    public class CalculateCircularBeamWithPiezoelectricVibration : CalculateBeamWithPiezoelectricVibration<CircularProfile>
    {
        /// <summary>
        /// Class contructor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        /// <param name="profileMapper"></param>
        /// <param name="mainMatrix"></param>
        /// <param name="commonMainMatrix"></param>
        public CalculateCircularBeamWithPiezoelectricVibration(
            INewmarkMethod newmarkMethod, 
            IMappingResolver mappingResolver,
            IProfileValidator<CircularProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation, 
            IProfileMapper<CircularProfile> profileMapper, 
            IBeamWithPiezoelectricMainMatrix<CircularProfile> mainMatrix,
            ICommonMainMatrix commonMainMatrix) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper, mainMatrix, commonMainMatrix)
        {
        }
    }
}
