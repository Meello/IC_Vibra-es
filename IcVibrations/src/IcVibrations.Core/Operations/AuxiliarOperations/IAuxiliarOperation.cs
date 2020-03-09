﻿namespace IcVibrations.Methods.AuxiliarOperations
{
    /// <summary>
    /// It contains auxiliar operations to the solve specific problems in the project.
    /// </summary>
    public interface IAuxiliarOperation
    {
        /// <summary>
        /// Applies the bondary conditions to a matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bondaryConditions"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        double[,] ApplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, uint size);

        /// <summary>
        /// Applies the bondary conditions to a vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="bondaryConditions"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        double[] ApplyBondaryConditions(double[] vector, bool[] bondaryConditions, uint size);

        /// <summary>
        /// Calculates the degrees freedom maximum.
        /// </summary>
        /// <param name="numberOfElements"></param>
        /// <returns></returns>
        uint CalculateDegreesFreedomMaximum(uint numberOfElements);

        /// <summary>
        /// Writes the values ​​corresponding to an instant of time in a file.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="values"></param>
        /// <param name="path"></param>
        void WriteInFile(double time, double[] values, string path);

        /// <summary>
        /// Writes the angular frequency in a file to start calculating the solution.
        /// </summary>
        /// <param name="angularFrequency"></param>
        /// <param name="path"></param>
        void WriteInFile(double angularFrequency, string path);
    }
}