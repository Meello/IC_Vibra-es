using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric
{
    public class PiezoelectricRequestData
    {
        public double YoungModulus { get; set; }

        public double PiezoelectricConstant { get; set; }
        
        public double DielectricConstant { get; set; }

        public double DielectricPermissiveness { get; set; }

        public double ElasticityConstant { get; set; }

        public double ElementLength { get; set; }

        public double SpecificMass { get; set; }

        public int[] ElementsWithPiezoelectric { get; set; }
        
        public double Thickness { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
