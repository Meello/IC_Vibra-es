using IC_Vibration.InputData.Beam;
using IC_Vibration.InputData.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibration.Calculus.PrincipalMatrixes
{
    public interface IPrincipalMatrix
    {
        double[,] Mass(int degreesFreedomPerElemenent, int degreesFreedomMaximum, int elements, double[] area, int[,] elementsCoordinate, double density, double length);

        double[,] Hardness(int degreesFreedomPerElemenent, int degreesFreedomMaximum, int elements, int[,] elementsCoordinate, double[] momentInertia, double[] youngModulus, double length);

        double[,] Damping(double[,] mass, double[,] hardness, double mi, int degreesFreedomMaximum); int[,] ElementsCoordinate(int elements, int dimensions);

        double[] Area(double area, int elements);

        double[] MomentInertia(double momentInertia, int elements);

        double[] YoungModulus(double youngModulus, int elements);

        double[] Force(double forca, int posicaoForca, int degreesFreedomMaximum);

        bool[] BondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
