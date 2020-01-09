using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] Mass(int degreesFreedomMaximum, int elements, double[,] massElement);

        double[,] Hardness(int degreesFreedomMaximum, int elements, double[,] hardnessElement);

        double[,] Damping(double[,] mass, double[,] hardness, double mi, int degreesFreedomMaximum);

        double[,] MassElement(double area, double density, double length);

        double[,] HardnessElement(double momentInertia, double youngModulus, double length);

        double[] Area(double area, int elements);

        double[] MomentInertia(double momentInertia, int elements);

        double[] YoungModulus(double youngModulus, int elements);

        double[] Force(double[] forceValues, int[] forcePositions, int degreesFreedomMaximum);

        bool[] BondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
