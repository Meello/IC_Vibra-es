using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.DTO
{
    public class NewmarkMethodInput
    {
        public double[,] Mass { get; set; }

        public double[,] Hardness { get; set; }

        public double[,] Damping { get; set; }

        public double[] Force { get; set; }

        public int DegreesFreedomMaximum { get; set; }
    }
}
