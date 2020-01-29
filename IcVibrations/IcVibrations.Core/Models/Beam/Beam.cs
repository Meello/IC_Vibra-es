using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Models.Beam
{
    public abstract class Beam
    {
        public uint ElementCount { get; set; }

        public Material Material { get; set; }

        public GeometricProperty GeometricProperty { get; set; }

        public Fastening FirstFastening { get; set; }

        public Fastening LastFastening { get; set; }

        public double Length { get; set; }

        public double[] Forces { get; set; }

        public double Thickness { get; set; }

        //public double[,] Mass { get; set; }

        //public double[,] Hardness { get; set; }

        //public double[,] Damping { get; set; }
    }

    public class CircularBeam : Beam
    {
        public double Diameter { get; set; }
    }

    public class RectangularBeam : Beam
    {
        public double Height { get; set; }

        public double Width { get; set; }
    }
}
