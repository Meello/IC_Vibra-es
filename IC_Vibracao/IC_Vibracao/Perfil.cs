using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public abstract class Perfil
    {
        public double Parede{ get; set; }
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
