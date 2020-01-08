using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] Mass (int degreesFreedomPerElemenent, int degreesFreedomMaximum, int elements, double[] area, int[,] elementsCoordinate, double density, double length);

        double[,] Hardness(int degreesFreedomPerElemenent, int degreesFreedomMaximum, int elements, int[,] elementsCoordinate, double[] momentInertia, double[] youngModulus, double length);

        double[,] Damping(double[,] mass, double[,] hardness, double mi, int degreesFreedomMaximum);
        
        int[,] ElementsCoordinate(int elements, int dimensions);

        double[] Area(double area, int elements);

        double[] MomentInertia(double momentInertia, int elements);

        double[] YoungModulus(double youngModulus, int elements);

        double[] Force(double[] forceValues, int[] forcePositions, int degreesFreedomMaximum);

        bool[] BondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
