using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Beam.Properties
{
    public abstract class Profile
    {
        public double Espessura { get; set; }

        public double Area { get; set; }

        public double MomentoInercia { get; set; }
    }

    public class PerfilRetangular : Profile
    {
        public double Altura { get; set; }

        public double Largura { get; set; }
    }

    public class PerfilCircular : Profile
    {
        public double Diametro { get; set; }
    }
}
