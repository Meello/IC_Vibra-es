﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.AuxiliarMethods
{
    public interface IAuxiliarMethod
    {
        double[,] AplyBondaryConditions(double[,] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue);

        double[] AplyBondaryConditions(double[] matriz, bool[] condicoesContorno, int n, int condicoesContornoTrue);
    }
}