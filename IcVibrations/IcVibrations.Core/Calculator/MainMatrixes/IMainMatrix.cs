using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] CalculateMass(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] CalculateHardness(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] CalculateDamping(double[,] mass, double[,] hardness, int degreesFreedomMaximum);

        double[,] CalculateMassElement(double area, double density, double lengthElement);

        double[,] CalculateHardnessElement(double momentInertia, double youngModulus, double lengthElement);

        double[] CalculateForce(double[] forceValues, int[] forceNodePositions, int degreesFreedomMaximum);

        bool[] CalculateBondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
