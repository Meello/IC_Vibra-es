using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Models.Beam.Characteristics
{
    public abstract class Profile
    {
        public double Thickness { get; set; }

        public double[] Area { get; set; }

        public double[] MomentInertia { get; set; }
    }

    public class RectangularProfile : Profile
    {
        public double Height { get; set; }

        public double Width { get; set; }
    }

    public class CircularProfile : Profile
    {
        public double Diameter { get; set; }
    }
}
