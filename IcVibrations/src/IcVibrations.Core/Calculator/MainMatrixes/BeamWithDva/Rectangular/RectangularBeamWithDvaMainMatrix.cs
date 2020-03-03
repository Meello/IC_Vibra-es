using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva.Rectangular
{
    /// <summary>
    /// It's responsible to calculate the rectangular beam with DVA main matrixes.
    /// </summary>
    public class RectangularBeamWithDvaMainMatrix : BeamWithDvaMainMatrix<RectangularProfile>, IRectangularBeamWithDvaMainMatrix
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        public RectangularBeamWithDvaMainMatrix(IArrayOperation arrayOperation) : base(arrayOperation) { }
    }
}
