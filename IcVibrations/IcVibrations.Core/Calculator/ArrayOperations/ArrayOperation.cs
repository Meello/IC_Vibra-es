using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IcVibrations.Core.Calculator.ArrayOperations
{
    public class ArrayOperation : IArrayOperation
    {
        public double[] Create(double value, uint size)
        {
            double[] newArray = new double[size];

            for (int i = 0; i < size; i++)
            {
                newArray[i] = value;
            }

            return newArray;
        }

        public double[,] InverseMatrix(double[,] matrix)
        {
            if(matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new Exception("It is just possible to inverse a qudratic matrix.");
            }

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

        public double[,] Multiply(double[,] array1, double[,] array2)
        {
            int rows1 = array1.GetLength(0);
            int columns1 = array1.GetLength(1);
            int rows2 = array2.GetLength(0);
            int columns2 = array2.GetLength(1);

            if(columns1 != rows2)
            {
                throw new Exception("Error in multiplication operation.");
            }

            double[,] arrayMultiplication = new double[rows1, columns2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < columns1; k++)
                    {
                        sum += array1[i, k] * array2[k, j];
                    }

                    arrayMultiplication[i, j] = sum;
                }
            }

            return arrayMultiplication;
        }

        public double[] Multiply(double[,] array1, double[] array2)
        {
            int rows1 = array1.GetLength(0);
            int columns1 = array1.GetLength(1);
            int size2 = array2.Length;

            if (columns1 != size2)
            {
                throw new Exception("Error in multiplication operation.");
            }

            double[] arrayMultiplication = new double[rows1];

            for (int i = 0; i < rows1; i++)
            {
                double sum = 0;

                for (int j = 0; j < columns1; j++)
                {
                    sum += array1[i, j] * array2[j];
                }

                arrayMultiplication[i] = sum;
            }

            return arrayMultiplication;
        }

        public double[] Multiply(double[] array1, double[,] array2)
        {
            int size1 = array1.Length;
            int rows2 = array2.GetLength(0);
            int columns2 = array2.GetLength(1);

            if (size1 != rows2)
            {
                throw new Exception("Error in multiplication operation.");
            }

            double[] arrayMultiplication = new double[rows2];

            for (int i = 0; i < columns2; i++)
            {
                double sum = 0;

                for (int j = 0; j < size1; j++)
                {
                    sum += array1[j] * array2[j,i];
                }

                arrayMultiplication[i] = sum;
            }

            return arrayMultiplication;
        }

        public double[,] Subtract(double[,] array1, double[,] array2)
        {
            int rows1 = array1.GetLength(0);
            int columns1 = array1.GetLength(1);
            int rows2 = array2.GetLength(0);
            int columns2 = array2.GetLength(1);

            if(rows1 != rows2 || columns1 != columns2)
            {
                throw new Exception("Can't subtract matrixes with differents sizes.");
            }

            double[,] arraySubtraction = new double[rows1, columns1];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    arraySubtraction[i, j] = array1[i, j] - array2[i, j];
                }
            }

            return arraySubtraction;
        }

        public double[] Subtract(double[] array1, double[] array2)
        {
            int size1 = array1.Length;
            int size2 = array2.Length;

            if (size1 != size2)
            {
                throw new Exception("Can't subtract matrixes with differents sizes.");
            }

            double[] arraySubtraction = new double[size1];

            for (int i = 0; i < size1; i++)
            {
                arraySubtraction[i] = array1[i] - array2[i];
            }

            return arraySubtraction;
        }

        public double[,] Sum(double[,] array1, double[,] array2)
        {
            int rows1 = array1.GetLength(0);
            int columns1 = array1.GetLength(1);
            int rows2 = array2.GetLength(0);
            int columns2 = array2.GetLength(1);

            if (rows1 != rows2 || columns1 != columns2)
            {
                throw new Exception("Can't sum matrixes with differents sizes.");
            }

            double[,] arraySum = new double[rows1, columns1];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    arraySum[i, j] = array1[i, j] + array2[i, j];
                }
            }

            return arraySum;
        }

        public double[] Sum(double[] array1, double[] array2)
        {
            int size1 = array1.Length;
            int size2 = array2.Length;

            if (size1 != size2)
            {
                throw new Exception("Can't sum matrixes with differents sizes.");
            }

            double[] arraySum = new double[size1];

            for (int i = 0; i < size1; i++)
            {
                arraySum[i] = array1[i] + array2[i];
            }

            return arraySum;
        }

        public double[,] TransposeMatrix(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);
            double[,] matrixTransposed = new double[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrixTransposed[j, i] = matrix[i, j];
                }
            }

            return matrixTransposed;
        }
    }
}
