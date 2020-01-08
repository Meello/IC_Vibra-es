using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.AuxiliarMethods
{
    public class AuxiliarMethod : IAuxiliarMethod
    {
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
