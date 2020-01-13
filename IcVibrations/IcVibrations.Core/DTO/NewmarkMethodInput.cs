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

        //public int DegreesFreedomMaximum { get; set; }

        //public int BondaryConditionTrueCount { get; set; }

        public double InitialTime { get; set; }

        public double TimeDivion { get; set; }

        public double FinalTime { get; set; }

        public double InitialAngularFrequency { get; set; }

        public double AngularFrequencyDivision { get; set; }

        public double FinalAngularFrequency { get; set; }
    }
}
