using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam
{
    public class BeamRequestData
    {
        public string Material { get; set; }

        public double Thickness { get; set; }

        public string FirstFastening { get; set; }

        public string LastFastening { get; set; }

        public double Length { get; set; }

        public double[] Forces { get; set; }

        public int[] ForceNodePositions { get; set; }
    }
}
