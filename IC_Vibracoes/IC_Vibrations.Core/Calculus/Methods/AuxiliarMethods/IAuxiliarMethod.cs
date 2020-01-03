using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibration.Calculus.Methods.AuxiliarMethods
{
    public interface IAuxiliarMethod
    {
        double[,] InverseMatrix(double[,] matriz, int n);

        double[,] AplyBondaryConditions(double[,] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue);

        double[] AplyBondaryConditions(double[] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue);
    }
}
