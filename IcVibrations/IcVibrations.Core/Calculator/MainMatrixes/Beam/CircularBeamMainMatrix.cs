using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;

namespace IcVibrations.Core.Calculator.MainMatrixes.Beam
{
    /// <summary>
    /// It's responsible to calculate the circular beam main matrixes.
    /// </summary>
    public class CircularBeamMainMatrix : BeamMainMatrix<CircularProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="commonMainMatrix"></param>
        public CircularBeamMainMatrix(ICommonMainMatrix commonMainMatrix) : base(commonMainMatrix)
        {
        }
    }
}
