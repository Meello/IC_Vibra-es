﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam
{
    public class BeamRequestData
    {
        public uint Nodes { get; set; }

        public string Material { get; set; }

        public string Profile { get; set; }
        
        public double? Diameter { get; set; }

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double Thickness { get; set; }

        public string Fastening1 { get; set; }

        public string FasteningN { get; set; }

        public double Length { get; set; }
    }
}
