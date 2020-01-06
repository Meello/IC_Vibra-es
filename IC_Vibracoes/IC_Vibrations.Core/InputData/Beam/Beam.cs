using IC_Vibration.InputData.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibration.InputData.Beam
{
    public class Beam
    {
        public Material Material { get; set; }

        public Profile Profile { get; set; }

        public Fastening Fastening1 { get; set; }

        public Fastening FasteningN { get; set; }

        //prefixo u --> somente positivos
        public uint Length { get; set; }

        public double[,] Mass { get; set; }

        public double[,] Hardness { get; set; }

        public double[,] Damping { get; set; }
    }
}
