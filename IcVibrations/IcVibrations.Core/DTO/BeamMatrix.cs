using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.DTO
{
    public class BeamMatrix
    {
        public double[,] Mass { get; set; }

        public double[,] Hardness { get; set; }
      
        public double[,] Damping { get; set; }

        public double[] Area { get; set; }

        public double[] MomentInertia { get; set; }
    }
}
