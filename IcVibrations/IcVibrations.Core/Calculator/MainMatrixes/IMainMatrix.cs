using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] BuildMass(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] BuildHardness(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] BuildDamping(double[,] mass, double[,] hardness, int degreesFreedomMaximum);

        double[,] BuildMassElement(double area, double density, double lengthElement);

        double[,] BuildHardnessElement(double momentInertia, double youngModulus, double lengthElement);

        double[] BuildForce(double[] forceValues, int[] forceNodePositions, int degreesFreedomMaximum);

        bool[] BuildBondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
