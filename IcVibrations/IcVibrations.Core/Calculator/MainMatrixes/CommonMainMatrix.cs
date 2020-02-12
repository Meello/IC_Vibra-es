﻿using IcVibrations.Core.Models;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Calculator.MainMatrixes
{
    public class CommonMainMatrix : ICommonMainMatrix
    {
        /// <summary>
        /// It's responsible to calculate the element mass matrix.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="specificMass"></param>
        /// <param name="elementLength"></param>
        /// <returns></returns>
        public Task<double[,]> CalculateElementMass(double area, double specificMass, double length)
        {
            double[,] elementMass = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

            double constant = area * specificMass * length / 420;

            elementMass[0, 0] = 156 * constant;
            elementMass[0, 1] = 22 * length * constant;
            elementMass[0, 2] = 54 * constant;
            elementMass[0, 3] = -13 * length * constant;
            elementMass[1, 0] = 22 * length * constant;
            elementMass[1, 1] = 4 * length * length * constant;
            elementMass[1, 2] = 13 * length * constant;
            elementMass[1, 3] = -3 * length * length * constant;
            elementMass[2, 0] = 54 * constant;
            elementMass[2, 1] = 13 * length * constant;
            elementMass[2, 2] = 156 * constant;
            elementMass[2, 3] = -22 * length * constant;
            elementMass[3, 0] = -13 * length * constant;
            elementMass[3, 1] = -3 * length * length * constant;
            elementMass[3, 2] = -22 * length * constant;
            elementMass[3, 3] = 4 * length * length * constant;

            return Task.FromResult(elementMass);
        }

        /// <summary>
        /// It's responsible to calculate the damping matrix.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="hardness"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Task<double[,]> CalculateDamping(double[,] mass, double[,] hardness, uint size)
        {
            double[,] damping = new double[size, size];

            for (uint i = 0; i < size; i++)
            {
                for (uint j = 0; j < size; j++)
                {
                    damping[i, j] = Constants.Alpha * mass[i, j] + Constants.Mi * hardness[i, j];
                }
            }

            return Task.FromResult(damping);
        }

        /// <summary>
        /// It's rewsponsible to build the bondary condition matrix.
        /// </summary>
        /// <param name="firstFastening"></param>
        /// <param name="lastFastening"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        public Task<bool[]> CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum)
        {
            bool[] bondaryCondition = new bool[degreesFreedomMaximum];

            for (uint i = 0; i < degreesFreedomMaximum; i++)
            {
                if (i == 0 || i == degreesFreedomMaximum - 2)
                {
                    bondaryCondition[i] = firstFastening.Displacement;
                }
                else if (i == 1 || i == degreesFreedomMaximum - 1)
                {
                    bondaryCondition[i] = firstFastening.Angle;
                }
                else
                {
                    bondaryCondition[i] = true;
                }
            }

            return Task.FromResult(bondaryCondition);
        }

        /// <summary>
        /// It's rewsponsible to build the bondary condition matrix.
        /// </summary>
        /// <param name="firstFastening"></param>
        /// <param name="lastFastening"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <param name="piezoelectricDegreesFreedomMaximum"></param>
        /// <returns></returns>
        public Task<bool[]> CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum)
        {
            bool[] bondaryCondition = new bool[degreesFreedomMaximum + piezoelectricDegreesFreedomMaximum];

            for (uint i = 0; i < degreesFreedomMaximum; i++)
            {
                if (i == 0 || i == degreesFreedomMaximum - 2)
                {
                    bondaryCondition[i] = firstFastening.Displacement;
                }
                else if (i == 1 || i == degreesFreedomMaximum - 1)
                {
                    bondaryCondition[i] = firstFastening.Angle;
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