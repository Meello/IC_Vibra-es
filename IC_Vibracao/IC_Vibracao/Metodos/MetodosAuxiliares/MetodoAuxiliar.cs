using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.Metodos.MetodosAuxiliares
{
    public class MetodoAuxiliar : IMetodoAuxiliar
    {
		public double[,] MatrizInversa(double[,] matriz, int n) 
        {
            double[,] matrizInv = new double[n, n];
            double pivot, p;
            int i, j, k, l;

            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrizInv[i, j] = 1;
                    }
                    else
                    {
                        matrizInv[i, j] = 0;
                    }
                }
            }

            //Triangularização
            for (i = 0; i < n; i++)
            {
                pivot = matriz[i, i];

                for (l = 0; l < n; l++)
                {
                    matriz[i, l] = matriz[i, l] / pivot;
                    matrizInv[i, l] = matrizInv[i, l] / pivot;
                }

                for (k = i + 1; k < n; k++)
                {
                    p = matriz[k, i];

                    for (j = 0; j < n; j++)
                    {
                        matriz[k, j] = matriz[k, j] - (p * matriz[i, j]);
                        matrizInv[k, j] = matrizInv[k, j] - (p * matrizInv[i, j]);
                    }
                }
            }

            //Retrosubstiuição
            for (i = n - 1; i >= 0; i--)
            {
                for (k = i - 1; k >= 0; k--)
                {
                    p = matriz[k, i];

                    for (j = n - 1; j >= 0; j--)
                    {
                        matriz[k, j] = matriz[k, j] - (p * matriz[i, j]);
                        matrizInv[k, j] = matrizInv[k, j] - (p * matrizInv[i, j]);
                    }
                }
            }

            return matrizInv;
		}
    }
}
