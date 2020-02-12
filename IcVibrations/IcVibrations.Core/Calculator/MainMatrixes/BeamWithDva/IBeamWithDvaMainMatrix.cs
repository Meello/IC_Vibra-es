using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva
{
    public interface IBeamWithDvaMainMatrix
    {
        /// <summary>
        /// Responsible to calculate the mass matrix of the beam.
        /// </summary>
        /// <param name="beam"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        Task<double[,]> CalculateMassWithDva(double[,] beamMass, double[] dvaMasses, uint[] dvaNodePositions);

        /// <summary>
        /// Responsible to calculate the hardness matrix of the beam.
        /// </summary>
        /// <param name="beam"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        Task<double[,]> CalculateBeamHardnessWithDva(double[,] beamHardness, double[] dvaHardness, uint[] dvaNodePositions);

        /// <summary>
        /// It's responsible to calculate the beam damping matrix.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="hardness"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        Task<double[,]> CalculateDamping(double[,] mass, double[,] hardness, uint degreesFreedomMaximum);
    }
}
