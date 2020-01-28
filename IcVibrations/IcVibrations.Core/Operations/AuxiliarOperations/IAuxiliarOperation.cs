using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.AuxiliarOperations
{
    public interface IAuxiliarOperation
    {
        double[,] AplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, int trueBondaryConditionCount);

        double[] AplyBondaryConditions(double[] matrix, bool[] bondaryConditions, int trueBondaryConditionCount);

        //void WriteInFile(string path, NewmarkMethodOutput output);
    }
}