using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.AuxiliarMethods
{
    public interface IAuxiliarMethod
    {
        double[,] AplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, int trueBondaryConditionCount);

        double[] AplyBondaryConditions(double[] matrix, bool[] bondaryConditions, int trueBondaryConditionCount);
    }
}
