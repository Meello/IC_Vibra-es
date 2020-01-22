using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric
{
    public class PiezoelectricRequestData
    {
        public double Thickness { get; set; }

        public int[] ElementsWithPiezoelectric { get; set; }
    }

    public class CircularPiezoelectricRequestData : PiezoelectricRequestData
    {
        public double Diameter { get; set; }
    }

    public class RectangularPiezoelectricRequestData : PiezoelectricRequestData
    {
        public double Width { get; set; }

        public double Height { get; set; }
    }
}
