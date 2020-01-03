using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.DataContract
{
    public class BeamRequestData
    {
        public int Nodes { get; set; }

        public int NodesPerElement { get; set; }

        public int Dimensions { get; set; }

        public int DegreesFreedom { get; set; }

        public string Material { get; set; }

        public string Profile { get; set; }
        
        public double Diameter { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public string Fastening1 { get; set; }

        public string FasteningN { get; set; }

        public double Length { get; set; }
    }
}
