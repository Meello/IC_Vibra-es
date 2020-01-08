using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Calculator.MatrixOperations
{
    public class MatrixOperation : IMatrixOperation
    {
        public double[,] InverseMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
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

            // Triangularization
            for (i = 0; i < n; i++)
            {
                pivot = matrix[i, i];

                for (l = 0; l < n; l++)
                {
                    matrix[i, l] = matrix[i, l] / pivot;
                    matrizInv[i, l] = matrizInv[i, l] / pivot;
                }

                for (k = i + 1; k < n; k++)
                {
                    p = matrix[k, i];

                    for (j = 0; j < n; j++)
                    {
                        matrix[k, j] = matrix[k, j] - (p * matrix[i, j]);
                        matrizInv[k, j] = matrizInv[k, j] - (p * matrizInv[i, j]);
                    }
                }
            }

            // Retrosubstitution
            for (i = n - 1; i >= 0; i--)
            {
                for (k = i - 1; k >= 0; k--)
                {
                    p = matrix[k, i];

                    for (j = n - 1; j >= 0; j--)
                    {
                        matrix[k, j] = matrix[k, j] - (p * matrix[i, j]);
                        matrizInv[k, j] = matrizInv[k, j] - (p * matrizInv[i, j]);
                    }
                }
            }

            return matrizInv;
        }

        public double[,] Multiply(double[,] matrix1, double[,] matrix2)
        {
            int lines1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int lines2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            if(columns1 != lines2)
            {
                return null;
            }

            double[,] matrixMultiplication = new double[lines1, columns2];

            for (int i = 0; i < lines1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < columns1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }

                    matrixMultiplication[i, j] = sum;
                }
            }

            return matrixMultiplication;
        }

        public double[,] Subtract(double[,] matrix1, double[,] matrix2)
        {
            int lines1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int lines2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            if(lines1 != lines2 || columns1 != columns2)
            {
                return null;
            }

            double[,] matrixSubtraction = new double[lines1, columns1];

            for (int i = 0; i < lines1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    matrixSubtraction[i, j] = matrix1[i, j] - matrix1[i, j];
                }
            }

            return matrixSubtraction;
        }

        public double[,] Sum(double[,] matrix1, double[,] matrix2)
        {
            int lines1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int lines2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            if (lines1 != lines2 || columns1 != columns2)
            {
                return null;
            }

            double[,] matrixSubtraction = new double[lines1, columns1];

            for (int i = 0; i < lines1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    matrixSubtraction[i, j] = matrix1[i, j] + matrix1[i, j];
                }
            }

            return matrixSubtraction;
        }
    }
}
