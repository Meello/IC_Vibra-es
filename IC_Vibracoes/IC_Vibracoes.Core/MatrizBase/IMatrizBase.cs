using IC_Vibracao.InputData.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.MatrizBase
{
    public interface IMatrizBase
    {
        double[,] Massa(Beam barra);

        double[,] Rigidez(Beam barra);

        double[,] Amortecimento(double[,] massa, double[,] rigidez, double mi);
    }
}
