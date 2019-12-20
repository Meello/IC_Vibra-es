using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public class Barra
    {
        public Material Material { get; set; }

        public Perfil Perfil { get; set; }

        public Apoio Apoio0 { get; set; }

        public Apoio ApoioN { get; set; }

        //prefixo u --> somente positivos
        public uint QuantElementos { get; set; }

        public uint Comprimento { get; set; }
    }
}
