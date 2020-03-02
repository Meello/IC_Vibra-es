﻿using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.ArrayOperations
{
    public interface IArrayOperation
    {
        /// <summary>
        /// It's responsible to add values in a matrix passing the node positions of each value.
        /// </summary>
        /// <param name="matrixToAdd"></param>
        /// <param name="values"></param>
        /// <param name="nodePositions"></param>
        /// <param name="matrixName"></param>
        /// <returns></returns>
        Task<double[,]> AddValue(double[,] matrixToAdd, double[] values, uint[] nodePositions, string matrixName);

        /// <summary>
        /// It's responsible to create a matrix with a unique value in all positions with a size that is informed.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <param name="vectorName"></param>
        /// <returns></returns>
        Task<double[]> Create(double value, uint size, string vectorName);

        /// <summary>
        /// It's responsible to inverse a matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="matrixName"></param>
        /// <returns></returns>
        Task<double[,]> InverseMatrix(double[,] matrix, string matrixName);

        /// <summary>
        /// It's responsible to merge two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        Task<double[]> MergeVectors(double[] vector1, double[] vector2);

        /// <summary>
        /// It's responsible to merge two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        Task<bool[]> MergeVectors(bool[] vector1, bool[] vector2);

        /// <summary>
        /// It's responsible to multiplicate a matrix and a vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <param name="arraysName"></param>
        /// <returns></returns>
        Task<double[]> Multiply(double[,] matrix, double[] vector, string arraysName);

        /// <summary>
        /// It's responsible to multiplicate a vector and a matrix.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="matrix"></param>
        /// <param name="arraysName"></param>
        /// <returns></returns>
        Task<double[]> Multiply(double[] vector, double[,] matrix, string arraysName);

        /// <summary>
        /// It's responsible to subtract two matrixes.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="matrixesName"></param>
        /// <returns></returns>
        Task<double[,]> Subtract(double[,] matrix1, double[,] matrix2, string matrixesName);

        /// <summary>
        /// It's responsible to subtract two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="vectorsName"></param>
        /// <returns></returns>
        Task<double[]> Subtract(double[] vector1, double[] vector2, string vectorsName);

        /// <summary>
        /// It's responsible to sum two matrixes.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="matrixesName"></param>
        /// <returns></returns>
        Task<double[,]> Sum(double[,] matrix1, double[,] matrix2, string matrixesName);

        /// <summary>
        /// It's responsible to sum three vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="vector3"></param>
        /// <param name="vectorsName"></param>
        Task<double[]> Sum(double[] vector1, double[] vector2, double[] vector3, string vectorsName);

        /// <summary>
        /// It's responsible to sum two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="vectorsName"></param>
        /// <returns></returns>
        Task<double[]> Sum(double[] vector1, double[] vector2, string vectorsName);

        /// <summary>
        /// It's responsible to calculate the transposed matrix of a informed matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        Task<double[,]> TransposeMatrix(double[,] matrix);
    }
}
