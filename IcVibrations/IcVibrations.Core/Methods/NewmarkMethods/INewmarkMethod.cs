using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod
    {
        OperationResponseData Execute(double[,] mass, double[,] hardness, double[,] damping);
    }
}
