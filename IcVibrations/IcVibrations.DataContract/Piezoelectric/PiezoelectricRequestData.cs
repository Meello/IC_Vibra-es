using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric
{
    public class PiezoelectricRequestData
    {
        public double Height { get; set; }

        public double Width { get; set; }

        public int[] ElementWithPiezoelectric { get; set; }
    }
}
