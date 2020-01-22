using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam
{
    public abstract class CalculateBeamRequestData
    {
        // Substituir alguns parâmetros pelo BeamRequestData
        public int ElementCount { get; set; }

        public string Material { get; set; }

        public double Thickness { get; set; }

        public string FirstFastening { get; set; }

        public string LastFastening { get; set; }

        public double Length { get; set; }

        public double[] Forces { get; set; }

        public int[] ForceNodePositions { get; set; }

        public double InitialTime { get; set; }

        public int PeriodDivion { get; set; }

        public int PeriodCount { get; set; }

        public double InitialAngularFrequency { get; set; }

        public double DeltaAngularFrequency { get; set; }

        public double FinalAngularFrequency { get; set; }
    }

    public class CircularBeamRequestData : CalculateBeamRequestData
    {
        public double Diameter { get; set; }
    }

    public class RectangularBeamRequestData : CalculateBeamRequestData
    {
        public double Height { get; set; }

        public double Width { get; set; }
    }
}
