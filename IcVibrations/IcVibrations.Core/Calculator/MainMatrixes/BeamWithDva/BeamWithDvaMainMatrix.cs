using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the beam with DVA main matrixes.
    /// </summary>
    public class BeamWithDvaMainMatrix : CommonMainMatrix, IBeamWithDvaMainMatrix
    {
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="commonMainMatrix"></param>
        public BeamWithDvaMainMatrix(
            IArrayOperation arrayOperation)
        {
            this._arrayOperation = arrayOperation;
        }

        /// <summary>
        /// Responsible to calculate the mass matrix of the beam.
        /// </summary>
        /// <param name="beamMass"></param>
        /// <param name="dvaMasses"></param>
        /// <param name="dvaNodePositions"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculateMassWithDva(double[,] beamMass, double[] dvaMasses, uint[] dvaNodePositions)
        {
            double[,] massWithDva = new double[beamMass.GetLength(0) + dvaMasses.Length, beamMass.GetLength(1) + dvaMasses.Length];

            beamMass = await this._arrayOperation.AddValue(beamMass, dvaMasses, dvaNodePositions, "Beam Mass");

            for (int i = 0; i < beamMass.GetLength(0); i++)
            {
                for (int j = 0; j < beamMass.GetLength(1); j++)
                {
                    massWithDva[i, j] = beamMass[i, j];
                }
            }

            for (int i = beamMass.GetLength(0); i < beamMass.GetLength(0) + dvaMasses.Length; i++)
            {
                massWithDva[i, i] = dvaMasses[i - beamMass.GetLength(0)];
            }

            return massWithDva;
        }

        /// <summary>
        /// Responsible to calculate the hardness matrix of the beam.
        /// </summary>
        /// <param name="beamHardness"></param>
        /// <param name="dvaHardness"></param>
        /// <param name="dvaNodePositions"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculateHardnessWithDva(double[,] beamHardness, double[] dvaHardness, uint[] dvaNodePositions)
        {
            double[,] hardnessWithDva = new double[beamHardness.GetLength(0) + dvaHardness.Length, beamHardness.GetLength(1) + dvaHardness.Length];

            beamHardness = await this._arrayOperation.AddValue(beamHardness, dvaHardness, dvaNodePositions, "Beam Hardness");

            for (int i = 0; i < beamHardness.GetLength(0); i++)
            {
                for (int j = 0; j < beamHardness.GetLength(1); j++)
                {
                    hardnessWithDva[i, j] = beamHardness[i, j];

                }
            }

            for (int i = beamHardness.GetLength(0); i < beamHardness.GetLength(0) + dvaHardness.Length; i++)
            {
                hardnessWithDva[i, i] = dvaHardness[i - beamHardness.GetLength(0)];
                hardnessWithDva[dvaNodePositions[i], i] = -dvaHardness[i - beamHardness.GetLength(0)];
                hardnessWithDva[i, dvaNodePositions[i]] = -dvaHardness[i - beamHardness.GetLength(0)];
            }

            return hardnessWithDva;
        }
    }
}
