using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Barra.Propriedades
{
    public abstract class Perfil
    {
        public double Espessura { get; set; }

        public double Area { get; set; }

        public double MomentoInercia { get; set; }
    }

    public class PerfilRetangular : Perfil
    {
        public double Altura { get; set; }

        public double Largura { get; set; }
    }

    public class PerfilCircular : Perfil
    {
        public double Diametro { get; set; }
    }
}
