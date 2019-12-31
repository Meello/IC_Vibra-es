using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.Metodos.MetodosAuxiliares
{
    public interface IMetodoAuxiliar
    {
        double[,] MatrizInversa(double[,] matriz, int n);
    }
}
