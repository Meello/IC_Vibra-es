using IC_Vibracao.InputData;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Barra.PropriedadesGeometricas
{
    public class Matrizes : IMatrizes
    {
        public int[,] CoordenadaElementos()
        {
            int[,] coordenadaElementos = new int[,] { };

            for(int i = 0; i<Constantes.Variaveis.NumeroElementos; i++)
            {
                for(int j = 0; j<Constantes.Variaveis.NumeroDimensoes; j++)
                {
                    coordenadaElementos[i, j] = i + j + 1;
                }
            }

            return coordenadaElementos;
        }

        public double[] Area(double area)
        {
            double[] matrizArea = Array.Empty<double>();

            for(int i = 0; i < Constantes.Variaveis.NumeroElementos;i++)
            {
                matrizArea[i] = area; 
            }

            return matrizArea;
        }

        public double[] MomentoInercia(double momentoInercia)
        {
            double[] matrizMomentoInercia = Array.Empty<double>();

            for (int i = 0; i < Constantes.Variaveis.NumeroElementos; i++)
            {
                matrizMomentoInercia[i] = momentoInercia;
            }

            return matrizMomentoInercia;
        }
    }
}
