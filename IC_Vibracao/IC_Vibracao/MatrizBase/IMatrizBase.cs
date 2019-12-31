using IC_Vibracao.InputData.Barra;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.MatrizBase
{
    public interface IMatrizBase
    {
        double[,] Massa(Barra barra);

        double[,] Rigidez(Barra barra);

        double[,] Amortecimento(double[,] massa, double[,] rigidez, double mi);
    }
}
