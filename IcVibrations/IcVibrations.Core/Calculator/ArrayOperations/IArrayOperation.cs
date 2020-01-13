using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Calculator.ArrayOperations
{
    public interface IArrayOperation
    {
        double[,] InverseMatrix(double[,] matrix);

        double[,] Multiply(double[,] array1, double[,] array2);

        double[] Multiply(double[,] array1, double[] array2);

        double[] Multiply(double[] array1, double[,] array2);

        double[,] Subtract(double[,] array1, double[,] array2);
        
        double[] Subtract(double[] array1, double[] array2);

        double[,] Sum(double[,] array1, double[,] array2);
        
        double[] Sum(double[] array1, double[] array2);
        
        double[] Create(double value, int size);

        double[] AddValues(double[] array, int[] positions, double[] values);
    }
}
