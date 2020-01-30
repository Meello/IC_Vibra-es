using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber
{
    public class BeamWithDva : Beam
    {
        public double[] DvaMasses { get; set; }

        public double[] DvaHardnesses { get; set; }

        public uint[] DvaNodePositions { get; set; }
    }

    public class CircularBeamWithDva : BeamWithDva
    {
        public double Diameter { get; set; }
    }

    public class RectangularBeamWithDva : BeamWithDva
    {
        public double Height { get; set; }

        public double Width { get; set; }
    }
}
