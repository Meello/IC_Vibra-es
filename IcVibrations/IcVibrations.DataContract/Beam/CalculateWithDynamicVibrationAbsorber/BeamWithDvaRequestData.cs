using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.CalculateWithDynamicVibrationAbsorber
{
    public class BeamWithDvaRequestData : BeamRequestData
    {
        public double[] DvaMasses { get; set; }

        public double[] DvaHardnesses { get; set; }

        public int[] DvaNodePositions { get; set; }
    }

    public class CircularBeamWithDvaRequestData : BeamWithDvaRequestData
    {
        public double BeamDiameter { get; set; }
    }

    public class RectangularBeamWithDvaRequestData : BeamWithDvaRequestData
    {
        public double BeamHeight { get; set; }

        public double BeamWidth { get; set; }
    }
}
