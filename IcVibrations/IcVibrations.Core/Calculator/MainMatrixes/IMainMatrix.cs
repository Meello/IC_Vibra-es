using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] CreateMass(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] CreateHardness(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] CreateDamping(double[,] mass, double[,] hardness, int degreesFreedomMaximum);

        double[,] CreateMassElement(double area, double density, double lengthElement);

        double[,] CreateHardnessElement(double momentInertia, double youngModulus, double lengthElement);

        double[] CreateForce(double[] forceValues, int[] forceNodePositions, int degreesFreedomMaximum);

        bool[] CreateBondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
