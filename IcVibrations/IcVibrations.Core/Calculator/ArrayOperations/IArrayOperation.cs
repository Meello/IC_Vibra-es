using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Calculator.ArrayOperations
{
    public interface IArrayOperation
    {
        double[,] InverseMatrix(double[,] matrix);

        double[,] InverseMatrix(double[,] matrix, int size);

        double[,] Multiply(double[,] matrix1, double[,] matrix2);

        double[] Multiply(double[,] matrix, double[] array);

        double[] Multiply(double[] array, double[,] matrix);

        double[,] Subtract(double[,] matrix1, double[,] matrix2);
        
        double[] Subtract(double[] array1, double[] array2);

        double[,] Sum(double[,] matrix1, double[,] matrix2);
        
        double[] Sum(double[] array1, double[] array2);
        
        double[] Create(double value, uint size);

        double[] Create(double value, uint size, uint[] positions);

        double[,] TransposeMatrix(double[,] matrix);

        double[,] AddValue(double[,] matrixToAdd, double[] values, uint[] valueNodePositions);
    }
}
