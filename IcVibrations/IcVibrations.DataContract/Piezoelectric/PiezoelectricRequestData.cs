using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric
{
    public class PiezoelectricRequestData
    {
        public double SpecificMass { get; set; }

        public double Thickness { get; set; }

        public uint[] ElementsWithPiezoelectric { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
