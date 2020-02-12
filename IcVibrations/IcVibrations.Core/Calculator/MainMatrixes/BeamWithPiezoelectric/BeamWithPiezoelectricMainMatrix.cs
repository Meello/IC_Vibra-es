﻿using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.Piezoelectric;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the beam with piezoelectric main matrixes.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class BeamWithPiezoelectricMainMatrix<TProfile> : IBeamWithPiezoelectricMainMatrix<TProfile>
        where TProfile : Profile, new()
    {
        private readonly ICommonMainMatrix _commonMainMatrix;
        private readonly IBeamMainMatrix<TProfile> _beamMainMatrix;
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="commonMainMatrix"></param>
        /// <param name="beamMainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public BeamWithPiezoelectricMainMatrix(
            ICommonMainMatrix commonMainMatrix,
            IBeamMainMatrix<TProfile> beamMainMatrix,
            IArrayOperation arrayOperation)
        {
            this._commonMainMatrix = commonMainMatrix;
            this._beamMainMatrix = beamMainMatrix;
            this._arrayOperation = arrayOperation;
        }

        /// <summary>
        /// It's responsible to calculate piezoelectric mass matrix.
        /// </summary>
        /// <param name="beamWithPiezoelectric"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculateMass(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint degreesFreedomMaximum)
        {
            uint numberOfElements = beamWithPiezoelectric.NumberOfElements;
            uint dfe = Constants.DegreesFreedomElement;

            double[,] mass = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double elementLength = beamWithPiezoelectric.Length / numberOfElements;

            for (uint n = 0; n < numberOfElements; n++)
            {
                double[,] elementPiezoelectricMass = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];
                double[,] elementBeamMass = await this._commonMainMatrix.CalculateElementMass(beamWithPiezoelectric.GeometricProperty.Area[n], beamWithPiezoelectric.Material.SpecificMass, elementLength);

                if (beamWithPiezoelectric.ElementsWithPiezoelectric.Contains(n + 1))
                {
                    elementPiezoelectricMass = await this._commonMainMatrix.CalculateElementMass(beamWithPiezoelectric.Profile.Area.Value, beamWithPiezoelectric.PiezoelectricSpecificMass, elementLength);
                }

                for (uint i = (dfe / 2) * n; i < (dfe / 2) * n + dfe; i++)
                {
                    for (uint j = (dfe / 2) * n; j < (dfe / 2) * n + dfe; j++)
                    {
                        mass[i, j] += elementPiezoelectricMass[i - (dfe / 2) * n, j - (dfe / 2) * n];
                    }
                }
            }

            return mass;
        }

        /// <summary>
        /// It's responsible to calculate piezoelectric hardness matrix.
        /// </summary>
        /// <param name="beamWithPiezoelectric"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculateHardness(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint degreesFreedomMaximum)
        {
            uint numberOfElements = beamWithPiezoelectric.NumberOfElements;
            uint dfe = Constants.DegreesFreedomElement;

            double[,] hardness = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double length = beamWithPiezoelectric.Length / numberOfElements;

            for (uint n = 0; n < numberOfElements; n++)
            {
                double[,] piezoelectricElementHardness = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];
                double[,] beamElementHardness = await this._beamMainMatrix.CalculateElementHardness(beamWithPiezoelectric.GeometricProperty.MomentOfInertia[n], beamWithPiezoelectric.Material.YoungModulus, length);

                if (beamWithPiezoelectric.ElementsWithPiezoelectric.Contains(n + 1))
                {
                    piezoelectricElementHardness = await this.CalculatePiezoelectricElementHardness(beamWithPiezoelectric.ElasticityConstant, beamWithPiezoelectric.Profile.MomentOfInertia.Value, length);
                }

                for (uint i = (dfe / 2) * n; i < (dfe / 2) * n + dfe; i++)
                {
                    for (uint j = (dfe / 2) * n; j < (dfe / 2) * n + dfe; j++)
                    {
                        hardness[i, j] += beamElementHardness[i - (dfe / 2) * n, j - (dfe / 2) * n] + piezoelectricElementHardness[i - (dfe / 2) * n, j - (dfe / 2) * n];
                    }
                }
            }

            return hardness;
        }

        /// <summary>
        /// It's responsible to calculate piezoelectric element hardness matrix.
        /// </summary>
        /// <param name="elasticityConstant"></param>
        /// <param name="momentInertia"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Task<double[,]> CalculatePiezoelectricElementHardness(double elasticityConstant, double momentInertia, double length)
        {
            double[,] elementHardness = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

            double constant = momentInertia * elasticityConstant / Math.Pow(length, 3);

            elementHardness[0, 0] = 12 * constant;
            elementHardness[0, 1] = 6 * length * constant;
            elementHardness[0, 2] = -12 * constant;
            elementHardness[0, 3] = 6 * length * constant;
            elementHardness[1, 0] = 6 * length * constant;
            elementHardness[1, 1] = 4 * Math.Pow(length, 2) * constant;
            elementHardness[1, 2] = -(6 * length * constant);
            elementHardness[1, 3] = 2 * Math.Pow(length, 2) * constant;
            elementHardness[2, 0] = -(12 * constant);
            elementHardness[2, 1] = -(6 * length * constant);
            elementHardness[2, 2] = 12 * constant;
            elementHardness[2, 3] = -(6 * length * constant);
            elementHardness[3, 0] = 6 * length * constant;
            elementHardness[3, 1] = 2 * Math.Pow(length, 2) * constant;
            elementHardness[3, 2] = -(6 * length * constant);
            elementHardness[3, 3] = 4 * Math.Pow(length, 2) * constant;

            return Task.FromResult(elementHardness);
        }

        /// <summary>
        /// It's responsible to calculate piezoelectric electromechanical coupling matrix.
        /// </summary>
        /// <param name="beamWithPiezoelectric"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculatePiezoelectricElectromechanicalCoupling(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint degreesFreedomMaximum)
        {
            uint numberOfElements = beamWithPiezoelectric.NumberOfElements;
            double[,] piezoelectricElectromechanicalCoupling = new double[degreesFreedomMaximum, numberOfElements + 1];

            for (uint n = 0; n < numberOfElements; n++)
            {
                double[,] piezoelectricElementElectromechanicalCoupling = new double[Constants.DegreesFreedomElement, Constants.PiezoelectricElementMatrixSize];

                if (beamWithPiezoelectric.ElementsWithPiezoelectric.Contains(n + 1))
                {
                    piezoelectricElementElectromechanicalCoupling = await this.CalculatePiezoelectricElementElectromechanicalCoupling(beamWithPiezoelectric);
                }

                for (uint i = 2 * n; i < 2 * n + Constants.DegreesFreedomElement; i++)
                {
                    for (uint j = n; j < n + Constants.PiezoelectricElementMatrixSize; j++)
                    {
                        piezoelectricElectromechanicalCoupling[i, j] += piezoelectricElementElectromechanicalCoupling[i - 2 * n, j - n];
                    }
                }
            }

            return piezoelectricElectromechanicalCoupling;
        }

        /// <summary>
        /// It's responsible to calculate piezoelectric element electromechanical coupling matrix.
        /// </summary>
        /// <param name="beamWithPiezoelectric"></param>
        /// <returns></returns>
        public abstract Task<double[,]> CalculatePiezoelectricElementElectromechanicalCoupling(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric);

        /// <summary>
        /// It's responsible to calculate piezoelectric capacitance matrix.
        /// </summary>
        /// <param name="beamWithPiezoelectric"></param>
        /// <param name="numberOfElements"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculatePiezoelectricCapacitance(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint numberOfElements)
        {
            double[,] piezoelectricCapacitance = new double[numberOfElements + 1, numberOfElements + 1];
            double elementLenght = beamWithPiezoelectric.Length / beamWithPiezoelectric.NumberOfElements;

            for (uint n = 0; n < numberOfElements; n++)
            {
                double[,] piezoelectricElementCapacitance = new double[Constants.PiezoelectricElementMatrixSize, Constants.PiezoelectricElementMatrixSize];

                if (beamWithPiezoelectric.ElementsWithPiezoelectric.Contains(n - 1))
                {
                    piezoelectricElementCapacitance = await this.CalculateElementPiezoelectricCapacitance(beamWithPiezoelectric);
                }

                for (uint i = n; i < n + Constants.PiezoelectricElementMatrixSize; i++)
                {
                    for (uint j = n; j < n + Constants.PiezoelectricElementMatrixSize; j++)
                    {
                        piezoelectricCapacitance[i, j] += piezoelectricElementCapacitance[i - n, j - n];
                    }
                }
            }

            return piezoelectricCapacitance;
        }

        /// <summary>
        /// It's responsible to calculate element piezoelectric capacitance matrix.
        /// </summary>
        /// <param name="beamWithPiezoelectric"></param>
        /// <returns></returns>
        public abstract Task<double[,]> CalculateElementPiezoelectricCapacitance(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric);

        /// <summary>
        /// It's responsible to calculate equivalent mass matrix.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <param name="piezoelectricDegreesFreedomMaximum"></param>
        /// <returns></returns>
        public Task<double[,]> CalculateEquivalentMass(double[,] mass, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum)
        {
            uint matrixSize = degreesFreedomMaximum + piezoelectricDegreesFreedomMaximum;
            double[,] equivalentMass = new double[matrixSize, matrixSize];

            for (uint i = 0; i < matrixSize; i++)
            {
                for (uint j = 0; j < matrixSize; j++)
                {
                    if (i < degreesFreedomMaximum && j < degreesFreedomMaximum)
                    {
                        equivalentMass[i, j] = mass[i, j];
                    }
                    else
                    {
                        equivalentMass[i, j] = 0;
                    }
                }
            }

            return Task.FromResult(equivalentMass);
        }

        /// <summary>
        /// It's responsible to calculate equivalent hardness matrix.
        /// </summary>
        /// <param name="hardness"></param>
        /// <param name="piezoelectricElectromechanicalCoupling"></param>
        /// <param name="piezoelectricCapacitance"></param>
        /// <param name="degreesFreedomMaximum"></param>
        /// <param name="piezoelectricDegreesFreedomMaximum"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculateEquivalentHardness(double[,] hardness, double[,] piezoelectricElectromechanicalCoupling, double[,] piezoelectricCapacitance, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum)
        {
            uint matrixSize = degreesFreedomMaximum + piezoelectricDegreesFreedomMaximum;

            double[,] piezoelectricElectromechanicalCouplingTransposed = await this._arrayOperation.TransposeMatrix(piezoelectricElectromechanicalCoupling);

            double[,] equivalentHardness = new double[matrixSize, matrixSize];

            for (uint i = 0; i < matrixSize; i++)
            {
                for (uint j = 0; j < matrixSize; j++)
                {
                    if (i < degreesFreedomMaximum && j < degreesFreedomMaximum)
                    {
                        equivalentHardness[i, j] = hardness[i, j];
                    }
                    else if (i < degreesFreedomMaximum && j > degreesFreedomMaximum)
                    {
                        equivalentHardness[i, j] = piezoelectricElectromechanicalCoupling[i, j - degreesFreedomMaximum];
                    }
                    else if (i > degreesFreedomMaximum && j < degreesFreedomMaximum)
                    {
                        equivalentHardness[i, j] = piezoelectricElectromechanicalCouplingTransposed[i - degreesFreedomMaximum, j];
                    }
                    else if (i > degreesFreedomMaximum && j > degreesFreedomMaximum)
                    {
                        equivalentHardness[i, j] = piezoelectricCapacitance[i - degreesFreedomMaximum, j - degreesFreedomMaximum];
                    }
                }
            }

            return equivalentHardness;
        }
    
        /// <summary>
        /// It's responsible to build the damping matrix.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="hardness"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<double[,]> CalculateDamping(double[,] mass, double[,] hardness, uint size)
        {
            return await this._commonMainMatrix.CalculateDamping(mass, hardness, size);
        }
    }
}
