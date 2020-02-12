using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
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
    /// It's responsible to calculate the vibration in a circular beam with dynamic vibration absorber.
    /// </summary>
    public class CalculateCircularBeamWithDvaVibration : CalculateBeamWithDvaVibration<CircularProfile>
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
        public CalculateCircularBeamWithDvaVibration(
            INewmarkMethod newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<CircularProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation, 
            IProfileMapper<CircularProfile> profileMapper,
            IBeamWithDvaMainMatrix mainMatrix, 
            IBeamMainMatrix<CircularProfile> beamMainMatrix,
            ICommonMainMatrix commonMainMatrix) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation, profileMapper, mainMatrix, beamMainMatrix, commonMainMatrix)
        {
        }
    }
}
