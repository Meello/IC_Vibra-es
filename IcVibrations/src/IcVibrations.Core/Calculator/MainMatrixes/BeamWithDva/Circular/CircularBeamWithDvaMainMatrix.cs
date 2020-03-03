using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva.Circular
{
    /// <summary>
    /// It's responsible to calculate the circular beam with DVA main matrixes.
    /// </summary>
    public class CircularBeamWithDvaMainMatrix : BeamWithDvaMainMatrix<CircularProfile>, ICircularBeamWithDvaMainMatrix
    {
        /// <summary>
        /// Class connstructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        public CircularBeamWithDvaMainMatrix(IArrayOperation arrayOperation) : base(arrayOperation) { }
    }
}
