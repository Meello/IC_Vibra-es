using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Calculator.MatrixOperations
{
    public interface IMatrixOperation
    {
        double[,] InverseMatrix(double[,] matriz);

        double[,] Multiply(double[,] matrix1, double[,] matrix2);

        double[,] Subtract(double[,] matrix1, double[,] matrix2);

        double[,] Sum(double[,] matrix1, double[,] matrix2);
    }
}
