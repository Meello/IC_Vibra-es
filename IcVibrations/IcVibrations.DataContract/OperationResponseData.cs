using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts
{
    public class OperationResponseData
    {
        public double[,] Result { get; set; }

        public double[] Time { get; set; }
    }
}
