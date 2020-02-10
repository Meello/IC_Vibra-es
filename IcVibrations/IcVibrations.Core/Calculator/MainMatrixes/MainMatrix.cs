using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IcVibrations.Calculator.MainMatrixes
{
    public class MainMatrix : IMainMatrix
    {
        private readonly IArrayOperation _arrayOperation;

        public MainMatrix(
            IArrayOperation arrayOperation)
        {
            this._arrayOperation = arrayOperation;
        }

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

        public Task<double[,]> CalculateBeamElementHardness(double momentInertia, double youngModulus, double length)
        {
            double[,] elementHardness = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

            double constant = momentInertia * youngModulus / Math.Pow(length, 3);

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

        public Task<double[,]> CalculatePiezoelectricElementElectromechanicalCoupling(RectangularPiezoelectric piezoelectric, double beamHeight)
        {
            double[,] electromechanicalCoupling = new double[Constants.DegreesFreedomElement, Constants.PiezoelectricElementMatrixSize];

            double constant = -(piezoelectric.DielectricPermissiveness * piezoelectric.Width * piezoelectric.ElementLength / 2) * (2 * beamHeight * piezoelectric.Height + Math.Pow(piezoelectric.Height, 2));

            electromechanicalCoupling[0, 0] = 0;
            electromechanicalCoupling[0, 1] = 0;
            electromechanicalCoupling[1, 0] = -piezoelectric.ElementLength * constant;
            electromechanicalCoupling[1, 1] = piezoelectric.ElementLength * constant;
            electromechanicalCoupling[2, 0] = 0;
            electromechanicalCoupling[2, 1] = piezoelectric.ElementLength * constant;
            electromechanicalCoupling[3, 0] = piezoelectric.ElementLength * constant;
            electromechanicalCoupling[3, 1] = -piezoelectric.ElementLength * constant;

            return Task.FromResult(electromechanicalCoupling);
        }

        public Task<double[,]> CalculatePiezoelectricElementCapacitance(double area, double length, double heigth, double dielectricConstant)
        {
            double[,] piezoelectricCapacitance = new double[Constants.PiezoelectricElementMatrixSize, Constants.PiezoelectricElementMatrixSize];

            double constant = -dielectricConstant * area * length / Math.Pow(heigth, 2);

            piezoelectricCapacitance[0, 0] = constant;
            piezoelectricCapacitance[0, 1] = -constant;
            piezoelectricCapacitance[1, 0] = -constant;
            piezoelectricCapacitance[1, 1] = constant;

            return Task.FromResult(piezoelectricCapacitance);
        }

        public async Task<double[,]> CalculateMass(Beam beam, BeamWithPiezoelectric piezoelectric, uint degreesFreedomMaximum)
        {
            uint elementCount = beam.ElementCount;

            uint[,] nodeCoordinates = await this.NodeCoordinates(elementCount);

            uint p, q, r, s;

            double[,] mass = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double length = beam.Length / elementCount;

            for (uint n = 0; n < elementCount; n++)
            {
                double[,] piezoelectricElementMass = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];
                
                double[,] beamElementMass = await this.CalculateElementMass(beam.GeometricProperty.Area[n], beam.Material.SpecificMass, length);

                if (piezoelectric.ElementsWithPiezoelectric.Contains(n - 1))
                {
                    piezoelectricElementMass = await this.CalculateElementMass(piezoelectric.GeometricProperty.Area[n], piezoelectric.SpecificMass, length);
                }

                p = nodeCoordinates[n, 0];
                q = nodeCoordinates[n, 1];
                r = nodeCoordinates[n, 2];
                s = nodeCoordinates[n, 3];

                mass[p, p] += beamElementMass[0, 0] + piezoelectricElementMass[0, 0];
                mass[p, q] += beamElementMass[0, 1] + piezoelectricElementMass[0, 1];
                mass[p, r] += beamElementMass[0, 2] + piezoelectricElementMass[0, 2];
                mass[p, s] += beamElementMass[0, 3] + piezoelectricElementMass[0, 3];
                mass[q, p] += beamElementMass[1, 0] + piezoelectricElementMass[1, 0];
                mass[q, q] += beamElementMass[1, 1] + piezoelectricElementMass[1, 1];
                mass[q, r] += beamElementMass[1, 2] + piezoelectricElementMass[1, 2];
                mass[q, s] += beamElementMass[1, 3] + piezoelectricElementMass[1, 3];
                mass[r, p] += beamElementMass[2, 0] + piezoelectricElementMass[2, 0];
                mass[r, q] += beamElementMass[2, 1] + piezoelectricElementMass[2, 1];
                mass[r, r] += beamElementMass[2, 2] + piezoelectricElementMass[2, 2];
                mass[r, s] += beamElementMass[2, 3] + piezoelectricElementMass[2, 3];
                mass[s, p] += beamElementMass[3, 0] + piezoelectricElementMass[3, 0];
                mass[s, q] += beamElementMass[3, 1] + piezoelectricElementMass[3, 1];
                mass[s, r] += beamElementMass[3, 2] + piezoelectricElementMass[3, 2];
                mass[s, s] += beamElementMass[3, 3] + piezoelectricElementMass[3, 3];
            }

            return mass;
        }

        public async Task<double[,]> CalculateMass(Beam beam, uint degreesFreedomMaximum)
        {
            uint elementCount = beam.ElementCount;

            uint[,] nodeCoordinates = await this.NodeCoordinates(elementCount);

            uint p, q, r, s;

            double[,] mass = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double length = beam.Length / elementCount;

            for (uint n = 0; n < elementCount; n++)
            {
                double[,] elementMass = await this.CalculateElementMass(beam.GeometricProperty.Area[n], beam.Material.SpecificMass, length);

                p = nodeCoordinates[n, 0];
                q = nodeCoordinates[n, 1];
                r = nodeCoordinates[n, 2];
                s = nodeCoordinates[n, 3];

                mass[p, p] += elementMass[0, 0];
                mass[p, q] += elementMass[0, 1];
                mass[p, r] += elementMass[0, 2];
                mass[p, s] += elementMass[0, 3];
                mass[q, p] += elementMass[1, 0];
                mass[q, q] += elementMass[1, 1];
                mass[q, r] += elementMass[1, 2];
                mass[q, s] += elementMass[1, 3];
                mass[r, p] += elementMass[2, 0];
                mass[r, q] += elementMass[2, 1];
                mass[r, r] += elementMass[2, 2];
                mass[r, s] += elementMass[2, 3];
                mass[s, p] += elementMass[3, 0];
                mass[s, q] += elementMass[3, 1];
                mass[s, r] += elementMass[3, 2];
                mass[s, s] += elementMass[3, 3];
            }

            return mass;
        }

        public async Task<double[,]> CalculateBeamHardness(Beam beam, uint degreesFreedomMaximum)
        {
            uint elementCount = beam.ElementCount;

            uint[,] nodeCoordinates = await this.NodeCoordinates(elementCount);

            uint p, q, r, s;

            double[,] hardness = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double length = beam.Length / elementCount;

            for (uint n = 0; n < elementCount; n++)
            {
                double[,] elementHardness = await this.CalculateBeamElementHardness(beam.GeometricProperty.MomentOfInertia[n], beam.Material.YoungModulus, length);

                p = nodeCoordinates[n, 0];
                q = nodeCoordinates[n, 1];
                r = nodeCoordinates[n, 2];
                s = nodeCoordinates[n, 3];

                hardness[p, p] += elementHardness[0, 0];
                hardness[p, q] += elementHardness[0, 1];
                hardness[p, r] += elementHardness[0, 2];
                hardness[p, s] += elementHardness[0, 3];
                hardness[q, p] += elementHardness[1, 0];
                hardness[q, q] += elementHardness[1, 1];
                hardness[q, r] += elementHardness[1, 2];
                hardness[q, s] += elementHardness[1, 3];
                hardness[r, p] += elementHardness[2, 0];
                hardness[r, q] += elementHardness[2, 1];
                hardness[r, r] += elementHardness[2, 2];
                hardness[r, s] += elementHardness[2, 3];
                hardness[s, p] += elementHardness[3, 0];
                hardness[s, q] += elementHardness[3, 1];
                hardness[s, r] += elementHardness[3, 2];
                hardness[s, s] += elementHardness[3, 3];
            }

            return hardness;
        }

        public async Task<double[,]> CalculateHardness(Beam beam, BeamWithPiezoelectric piezoelectric, uint degreesFreedomMaximum)
        {
            uint elementCount = beam.ElementCount;

            uint[,] nodeCoordinates = await this.NodeCoordinates(elementCount);

            uint p, q, r, s;

            double[,] hardness = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double length = beam.Length / elementCount;

            for (uint n = 0; n < elementCount; n++)
            {
                double[,] piezoelectricElementHardness = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

                double[,] beamElementHardness = await this.CalculateBeamElementHardness(beam.GeometricProperty.MomentOfInertia[n], beam.Material.YoungModulus, length);
                
                if (piezoelectric.ElementsWithPiezoelectric.Contains(n - 1))
                {
                    piezoelectricElementHardness = await this.CalculatePiezoelectricElementHardness(piezoelectric.ElasticityConstant, piezoelectric.GeometricProperty.MomentOfInertia[n], length);
                }

                p = nodeCoordinates[n, 0];
                q = nodeCoordinates[n, 1];
                r = nodeCoordinates[n, 2];
                s = nodeCoordinates[n, 3];

                hardness[p, p] += beamElementHardness[0, 0] + piezoelectricElementHardness[0, 0];
                hardness[p, q] += beamElementHardness[0, 1] + piezoelectricElementHardness[0, 1];
                hardness[p, r] += beamElementHardness[0, 2] + piezoelectricElementHardness[0, 2];
                hardness[p, s] += beamElementHardness[0, 3] + piezoelectricElementHardness[0, 3];
                hardness[q, p] += beamElementHardness[1, 0] + piezoelectricElementHardness[1, 0];
                hardness[q, q] += beamElementHardness[1, 1] + piezoelectricElementHardness[1, 1];
                hardness[q, r] += beamElementHardness[1, 2] + piezoelectricElementHardness[1, 2];
                hardness[q, s] += beamElementHardness[1, 3] + piezoelectricElementHardness[1, 3];
                hardness[r, p] += beamElementHardness[2, 0] + piezoelectricElementHardness[2, 0];
                hardness[r, q] += beamElementHardness[2, 1] + piezoelectricElementHardness[2, 1];
                hardness[r, r] += beamElementHardness[2, 2] + piezoelectricElementHardness[2, 2];
                hardness[r, s] += beamElementHardness[2, 3] + piezoelectricElementHardness[2, 3];
                hardness[s, p] += beamElementHardness[3, 0] + piezoelectricElementHardness[3, 0];
                hardness[s, q] += beamElementHardness[3, 1] + piezoelectricElementHardness[3, 1];
                hardness[s, r] += beamElementHardness[3, 2] + piezoelectricElementHardness[3, 2];
                hardness[s, s] += beamElementHardness[3, 3] + piezoelectricElementHardness[3, 3];
            }

            return hardness;
        }

        public async Task<double[,]> CalculatePiezoelectricElectromechanicalCoupling(double beamHeight, uint elementCount, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum)
        {
            double[,] piezoelectricElectromechanicalCoupling = new double[degreesFreedomMaximum, elementCount + 1];

            for (uint n = 0; n < elementCount; n++)
            {
                double[,] piezoelectricElementElectromechanicalCoupling = new double[Constants.DegreesFreedomElement, Constants.PiezoelectricElementMatrixSize];

                if (piezoelectric.ElementsWithPiezoelectric.Contains(n - 1))
                {
                    piezoelectricElementElectromechanicalCoupling = await this.CalculatePiezoelectricElementElectromechanicalCoupling(piezoelectric, beamHeight);
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

        public async Task<double[,]> CalculatePiezoelectricCapacitance(RectangularPiezoelectric piezoelectric, uint elementCount)
        {
            double[,] piezoelectricCapacitance = new double[elementCount + 1, elementCount + 1];

            for (uint n = 0; n < elementCount; n++)
            {
                double[,] piezoelectricElementCapacitance = new double[Constants.PiezoelectricElementMatrixSize, Constants.PiezoelectricElementMatrixSize];

                if(piezoelectric.ElementsWithPiezoelectric.Contains(n - 1))
                {
                    piezoelectricElementCapacitance = await this.CalculatePiezoelectricElementCapacitance(piezoelectric.GeometricProperty.Area[n], piezoelectric.ElementLength, piezoelectric.Height, piezoelectric.DielectricConstant);
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

        public Task<double[,]> CalculateDamping(double[,] mass, double[,] hardness, uint degreesFreedomMaximum)
        {
            double[,] damping = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            for (uint i = 0; i < degreesFreedomMaximum; i++)
            {
                for (uint j = 0; j < degreesFreedomMaximum; j++)
                {
                    damping[i, j] = Constants.Alpha * mass[i, j] + Constants.Mi * hardness[i, j];
                }
            }

            return Task.FromResult(damping);
        }

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

        private Task<uint[,]> NodeCoordinates(uint elementCount)
        {
            uint[,] nodeCoordinates = new uint[elementCount + 1, Constants.DegreesFreedomElement];

            for (uint i = 0; i < elementCount + 1; i++)
            {
                for (uint j = 0; j < Constants.DegreesFreedomElement; j++)
                {
                    nodeCoordinates[i, j] = 2 * i + j;
                }
            }

            return Task.FromResult(nodeCoordinates);
        }

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

        public async Task<double[,]> CalculateBeamHardnessWithDva(double[,] beamHardness, double[] dvaHardness, uint[] dvaNodePositions)
        {
            double[,] hardnessWithDva = new double[beamHardness.GetLength(0) + dvaHardness.Length, beamHardness.GetLength(1) + dvaHardness.Length];

            beamHardness =  await this._arrayOperation.AddValue(beamHardness, dvaHardness, dvaNodePositions, "Beam Hardness");

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