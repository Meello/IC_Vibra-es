﻿using IcVibrations.Common.Classes;

namespace IcVibrations.Methods.AuxiliarOperations
{
    public interface IAuxiliarOperation
    {
        double[,] ApplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, uint size);

        double[] ApplyBondaryConditions(double[] matrix, bool[] bondaryConditions, uint size);

        uint CalculateDegreesFreedomMaximum(uint numberOfElements);

        //void WriteInFile(string path, Result result);

        void WriteInFile(string path, string message);
    }
}