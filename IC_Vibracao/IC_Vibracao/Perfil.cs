using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public abstract class Perfil
    {
        public double Espessura { get; set; }
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

    public class PropriedadesGeometricas
    {
        public double[] Area(PerfilCircular perfilCircular, int numeroElementos)
        {
            double[] area = new double[] { };

            double d = perfilCircular.Diametro;
            double t = perfilCircular.Espessura;

            for (int i = 0; i < numeroElementos; i++)
            {
                area[i] = (Math.PI / 4) * (Math.Pow(d, 2) - Math.Pow((d - 2*t),2));
            }

            return area;
        }

        public double[] Area(PerfilRetangular perfilRetangular, int numeroElementos)
        {
            double[] area = new double[] { };

            double a = perfilRetangular.Altura;
            double b = perfilRetangular.Largura;
            double t = perfilRetangular.Espessura;

            for (int i = 0; i < numeroElementos; i++)
            {
                area[i] = (a * b) - ((a - 2 * t) * (a - 2 * t));
            }

            return area;
        }

        public double[] MomentoInercia(PerfilCircular perfilCircular, int numeroElementos)
        {
            double[] momentoInercia = new double[] { };

            double d = perfilCircular.Diametro;
            double t = perfilCircular.Espessura;

            for (int i = 0; i < numeroElementos; i++)
            {
                momentoInercia[i] = (Math.PI / 64) * (Math.Pow(d, 4) - Math.Pow((d - 2 * t), 4));
            }

            return momentoInercia;
        }

        public double[] MomentoInercia(PerfilRetangular perfilRetangular, int numeroElementos)
        {
            double[] momentoInercia = new double[] { };

            double a = perfilRetangular.Altura;
            double b = perfilRetangular.Largura;
            double t = perfilRetangular.Espessura;

            for (int i = 0; i < numeroElementos; i++)
            {
                momentoInercia[i] = (Math.Pow(a, 3) * b - (Math.Pow((a - 2 * t), 3) * (b - 2 * t))) / 12;
            }

            return momentoInercia;
        }
    }
}
