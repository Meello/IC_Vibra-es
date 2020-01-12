using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.AuxiliarMethods
{
    public interface IAuxiliarMethod
    {
        double[,] AplyBondaryConditions(double[,] matrix, bool[] bondaryConditions);

        double[] AplyBondaryConditions(double[] matrix, bool[] bondaryConditions);
    }
}
