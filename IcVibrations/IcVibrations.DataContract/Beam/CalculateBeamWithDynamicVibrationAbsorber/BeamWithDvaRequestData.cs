using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber
{
    public class BeamWithDvaRequestData : BeamRequestData
    {
        public double[] DvaMasses { get; set; }

        public double[] DvaHardnesses { get; set; }

        public uint[] DvaNodePositions { get; set; }
    }

    public class CircularBeamWithDvaRequestData : BeamWithDvaRequestData
    {
        public double Diameter { get; set; }
    }

    public class RectangularBeamWithDvaRequestData : BeamWithDvaRequestData
    {
        public double Height { get; set; }

        public double Width { get; set; }
    }
}
