using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.DTO
{
    public class NewmarkMethodOutput
    {
        public double[,] Result { get; set; }

        public double[] Time { get; set; }
    }
}
