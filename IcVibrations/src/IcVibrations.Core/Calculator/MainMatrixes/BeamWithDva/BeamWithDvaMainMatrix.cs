﻿using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the beam with DVA main matrixes.
    /// </summary>
    public class BeamWithDvaMainMatrix<TProfile> : BeamMainMatrix<TProfile>, IBeamWithDvaMainMatrix
        where TProfile : Profile, new()
    {
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
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

            for (int i = 0; i < dvaMasses.Length; i++)
            {
                massWithDva[dvaNodePositions[i], dvaNodePositions[i]] = dvaMasses[i];
                massWithDva[i + beamMass.GetLength(0), i + beamMass.GetLength(0)] = dvaMasses[i];
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

            for (int i = 0; i < dvaHardness.Length; i++)
            {
                hardnessWithDva[dvaNodePositions[i], dvaNodePositions[i]] += dvaHardness[i];
                hardnessWithDva[i + beamHardness.GetLength(0), i + beamHardness.GetLength(0)] = dvaHardness[i];
                hardnessWithDva[dvaNodePositions[i], i + beamHardness.GetLength(0)] = -dvaHardness[i];
                hardnessWithDva[i + beamHardness.GetLength(0), dvaNodePositions[i]] = -dvaHardness[i];
            }

            return hardnessWithDva;
        }

        public Task<bool[]> CalculateBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum, uint numberOfDvas)
        {
            uint size = degreesFreedomMaximum + numberOfDvas;
            bool[] bondaryCondition = new bool[size];

            for (uint i = 0; i < size; i++)
            {
                if (i == 0)
                {
                    bondaryCondition[i] = firstFastening.Displacement;
                }
                else if (i == degreesFreedomMaximum - 2)
                {
                    bondaryCondition[i] = lastFastening.Displacement;
                }
                else if (i == 1)
                {
                    bondaryCondition[i] = firstFastening.Angle;
                }
                else if (i == degreesFreedomMaximum - 1)
                {
                    bondaryCondition[i] = lastFastening.Angle;
                }
                else
                {
                    bondaryCondition[i] = true;
                }
            }

            return Task.FromResult(bondaryCondition);
        }
    }
}
