using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibration.Methods.AuxiliarMethods
{
    public class AuxiliarMethod : IAuxiliarMethod
    {
		public double[,] InverseMatrix(double[,] matriz, int n) 
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

        public double[,] AplyBondaryConditions(double[,] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue)
        {
            int i, j, k, count1, count2;

            double[,] matrizCC = new double[condicoesContornoTrue, condicoesContornoTrue];

            for (i = 0; i < n; i++)
            {
                if (condicoesContorno[i] == false)
                {
                    for (j = 0; j < n; j++)
                    {
                        matriz[i, j] = 0;
                    }

                    for (k = 0; k < n; k++) 
                    { 
                        matriz[k, i] = 0;
                    }
                }
            }

            count2 = 0;

            for (i = 0; i < n; i++)
            {
                count1 = 0;

                for (j = 0; j < n; j++)
                {
                    if (condicoesContorno[i] == true && condicoesContorno[j] == true)
                    {
                        matrizCC[count1, count2] = matriz[j, i];

                        count1 += 1;
                    }
                }

                if (condicoesContorno[i] == true)
                {
                    count2 += 1;
                }
            }

            return matrizCC;
        }

        public double[] AplyBondaryConditions(double[] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue)
        {
            int i, cont1 = 0;

            double[] matrizCC = new double[condicoesContornoTrue];

            for (i = 0; i < n; i++)
            {
                if (condicoesContorno[i] == true)
                {
                    matrizCC[cont1] = matriz[i];
                    cont1 += 1;
                }
            }

            return matrizCC;
        }
    }
}
