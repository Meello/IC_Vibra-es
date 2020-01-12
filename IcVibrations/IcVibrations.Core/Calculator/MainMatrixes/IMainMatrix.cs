using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] Mass(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] Hardness(Beam beam, int degreesFreedomMaximum, int elements);

        double[,] Damping(double[,] mass, double[,] hardness, double mi, int degreesFreedomMaximum);

        double[,] MassElement(double area, double density, double lengthElement);

        double[,] HardnessElement(double momentInertia, double youngModulus, double lengthElement);

        double[] Force(double[] forceValues, int[] forcePositions, int degreesFreedomMaximum);

        bool[] BondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
