using IcVibrations.Calculator.MainMatrixes;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the beam with DVA main matrixes.
    /// </summary>
    public interface IBeamWithDvaMainMatrix : ICommonMainMatrix
    {
        /// <summary>
        /// Responsible to calculate the mass matrix of the beam.
        /// </summary>
        /// <param name="beamMass"></param>
        /// <param name="dvaMasses"></param>
        /// <param name="dvaNodePositions"></param>
        /// <returns></returns>
        Task<double[,]> CalculateMassWithDva(double[,] beamMass, double[] dvaMasses, uint[] dvaNodePositions);

        /// <summary>
        /// Responsible to calculate the hardness matrix of the beam.
        /// </summary>
        /// <param name="beamHardness"></param>
        /// <param name="dvaHardness"></param>
        /// <param name="dvaNodePositions"></param>
        /// <returns></returns>
        Task<double[,]> CalculateHardnessWithDva(double[,] beamHardness, double[] dvaHardness, uint[] dvaNodePositions);
    }
}
