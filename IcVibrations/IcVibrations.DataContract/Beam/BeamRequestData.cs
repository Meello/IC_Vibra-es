using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam
{
    public abstract class BeamRequestData
    {
        public uint NodeCount { get; set; }

        public string Material { get; set; }

        public double Thickness { get; set; }

        public string FirstFastening { get; set; }

        public string LastFastening { get; set; }

        public double Length { get; set; }
    }

    public class CircularRequestData : BeamRequestData
    {
        public double? Diameter { get; set; }
    }

    public class RectangularRequestData : BeamRequestData
    {
        public double? Height { get; set; }

        public double? Width { get; set; }
    }
}
