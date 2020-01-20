using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.AuxiliarMethods
{
    public class AuxiliarMethod : IAuxiliarMethod
    {
        public double[,] AplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, int trueBondaryContionCount)
        {
            int i, j, k, count1, count2;

            int n = matrix.GetLength(0);

            double[,] matrixCC = new double[trueBondaryContionCount, trueBondaryContionCount];

            for (i = 0; i < n; i++)
            {
                if (bondaryConditions[i] == false)
                {
                    for (j = 0; j < n; j++)
                    {
                        matrix[i, j] = 0;
                    }

                    for (k = 0; k < n; k++) 
                    { 
                        matrix[k, i] = 0;
                    }
                }
            }

            count2 = 0;

            for (i = 0; i < n; i++)
            {
                count1 = 0;

                for (j = 0; j < n; j++)
                {
                    if (bondaryConditions[i] == true && bondaryConditions[j] == true)
                    {
                        matrixCC[count1, count2] = matrix[j, i];

                        count1 += 1;
                    }
                }

                if (bondaryConditions[i] == true)
                {
                    count2 += 1;
                }
            }

            return matrixCC;
        }

        public double[] AplyBondaryConditions(double[] matrix, bool[] bondaryConditions, int trueBondaryContionCount)
        {
            int i, count1 = 0;

            int n = matrix.GetLength(0);

            double[] matrixCC = new double[trueBondaryContionCount];

            for (i = 0; i < n; i++)
            {
                if (bondaryConditions[i] == true)
                {
                    matrixCC[count1] = matrix[i];
                    count1 += 1;
                }
            }

            return matrixCC;
        }
    }
}
