using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.DTO
{
    public class MassHardnessDamping
    {
        public double[,] Mass { get; set; }

        public double[,] Hardness { get; set; }
      
        public double[,] Damping { get; set; }
    }
}
