using IC_Vibracao.InputData.Beam.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Beam
{
    public class Beam
    {
        public Material Material { get; set; }

        public Profile Perfil { get; set; }

        public Fastening Apoio1 { get; set; }

        public Fastening ApoioN { get; set; }

        //prefixo u --> somente positivos
        public uint Comprimento { get; set; }
    }
}
