using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.Metodos.MetodosAuxiliares
{
    public interface IMetodoAuxiliar
    {
        double[,] MatrizInversa(double[,] matriz, int n);

        double[,] AplicarCondicoesContorno(double[,] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue);

        double[] AplicarCondicoesContorno(double[] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue);
    }
}
