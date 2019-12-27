using IC_Vibracao.InputData.Barra.Propriedades;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Barra
{
    public class Barra
    {
        public Material Material { get; set; }

        public Perfil Perfil { get; set; }

        public Fixacao Apoio1 { get; set; }

        public Fixacao ApoioN { get; set; }

        //prefixo u --> somente positivos
        public uint Comprimento { get; set; }
    }
}
