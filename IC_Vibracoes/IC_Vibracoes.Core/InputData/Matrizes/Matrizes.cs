using IC_Vibracao.InputData;
using IC_Vibracao.InputData.Beam.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using static IC_Vibracao.InputData.Constantes;

namespace IC_Vibracao.InputData.Beam.PropriedadesGeometricas
{
    public class Matrizes : IMatrizes
    {
        public int[,] CoordenadaElementos()
        {
            int[,] coordenadaElementos = new int[Variaveis.NumeroElementos, Constantes.Variaveis.NumeroDimensoes];

            for(int i = 0; i<Variaveis.NumeroElementos; i++)
            {
                for(int j = 0; j<Variaveis.NumeroDimensoes; j++)
                {
                    coordenadaElementos[i, j] = i + j + 1;
                }
            }

            return coordenadaElementos;
        }

        public double[] Area(double area, int n)
        {
            double[] matrizArea = new double[n];

            for(int i = 0; i < n;i++)
            {
                matrizArea[i] = area; 
            }

            return matrizArea;
        }

        public double[] MomentoInercia(double momentoInercia, int n)
        {
            double[] matrizMomentoInercia = new double[n];

            for (int i = 0; i < Variaveis.n; i++)
            {
                matrizMomentoInercia[i] = momentoInercia;
            }

            return matrizMomentoInercia;
        }

        public double[] ModuloElasticade(double moduloElasticidade)
        {
            double[] moduloEl = new double[Variaveis.NumeroElementos];

            for (int i = 0; i < Variaveis.NumeroElementos; i++)
            {
                moduloEl[i] = moduloElasticidade;
            }

            return moduloEl;
        }

        public double[] Forcamento(double forca, int posicaoForca)
        {
            double[] forcamento = new double[Variaveis.GrausLiberdadeMaximo];

            for (int i = 0; i < Variaveis.GrausLiberdadeMaximo; i++)
            {
                if(i == posicaoForca)
                {
                    forcamento[i] = forca;
                }
                else
                {
                    forcamento[i] = 0;
                }
            }

            return forcamento;
        }

        public bool[] CondicoesContorno(Fastening fixacao1, Fastening fixacaoN)
        {
            bool[] condicoesContorno = new bool[Variaveis.GrausLiberdadeMaximo];

            for (int i = 0; i < Variaveis.GrausLiberdadeMaximo; i++)
            {
                if(i == 0)
                {
                    condicoesContorno[i] = fixacao1.Deslocamento;
                    condicoesContorno[i + 1] = fixacao1.Angulo;
                }
                else if (i == Variaveis.GrausLiberdadeMaximo - 1)
                {
                    condicoesContorno[i - 1] = fixacaoN.Deslocamento;
                    condicoesContorno[i] = fixacaoN.Angulo;
                }
                else
                {
                    condicoesContorno[i] = true;
                }
            }

            return condicoesContorno;
        }
    }
}
