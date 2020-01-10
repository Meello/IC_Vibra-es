using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod
    {
        NewmarkMethodOutput Execute(double[,] mass, double[,] hardness, double[,] damping);
    }
}
