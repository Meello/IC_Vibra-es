using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Models.Piezoelectric
{
    public class Piezoelectric
    {
        public double YoungModulus { get; set; }

        // d31
        public double PiezoelectricConstant { get; set; }

        // k33
        public double DielectricConstant { get; set; }

        // e31
        public double DielectricPermissiveness { get; set; }

        // c11
        public double ElasticityConstant { get; set; }

        public double ElementLength { get; set; }
        
        public double SpecificMass { get; set; }

        public RectangularProfile Profile { get; set; }

        public int[] ElementsWithPiezoelectric { get; set; }
    }
}
