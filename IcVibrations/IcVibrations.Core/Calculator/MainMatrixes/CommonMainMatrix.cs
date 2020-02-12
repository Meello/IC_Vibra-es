using IcVibrations.Common.Profiles;
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
    public class CommonMainMatrix : ICommonMainMatrix
    {
        private readonly IArrayOperation _arrayOperation;

        public CommonMainMatrix(
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

        public async Task<double[,]> CalculateMass(Beam<Profile> beam, uint degreesFreedomMaximum)
        {
            uint numberOfElements = beam.NumberOfElements;

            uint[,] nodeCoordinates = await this.NodeCoordinates(numberOfElements);

            uint p, q, r, s;

            double[,] mass = new double[degreesFreedomMaximum, degreesFreedomMaximum];

            double length = beam.Length / numberOfElements;

            for (uint n = 0; n < numberOfElements; n++)
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
    }
}