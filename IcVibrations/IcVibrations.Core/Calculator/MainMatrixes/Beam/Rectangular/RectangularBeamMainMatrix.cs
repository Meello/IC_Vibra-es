using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;

namespace IcVibrations.Core.Calculator.MainMatrixes.Beam.Rectangular
{
    /// <summary>
    /// It's responsible to calculate the circular beam main matrixes.
    /// </summary>
    public class RectangularBeamMainMatrix : BeamMainMatrix<RectangularProfile>, IRectangularBeamMainMatrix
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="commonMainMatrix"></param>
        public RectangularBeamMainMatrix(ICommonMainMatrix commonMainMatrix) : base(commonMainMatrix)
        {
        }
    }
}
