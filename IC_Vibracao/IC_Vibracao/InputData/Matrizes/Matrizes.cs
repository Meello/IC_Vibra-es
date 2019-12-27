using IC_Vibracao.InputData;
using IC_Vibracao.InputData.Barra.Propriedades;
using System;
using System.Collections.Generic;
using System.Text;
using static IC_Vibracao.InputData.Constantes;

namespace IC_Vibracao.InputData.Barra.PropriedadesGeometricas
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

        public double[] Area(double area)
        {
            double[] matrizArea = new double[Variaveis.NumeroElementos];

            for(int i = 0; i < Variaveis.NumeroElementos;i++)
            {
                matrizArea[i] = area; 
            }

            return matrizArea;
        }

        public double[] MomentoInercia(double momentoInercia)
        {
            double[] matrizMomentoInercia = new double[Variaveis.NumeroElementos];

            for (int i = 0; i < Variaveis.NumeroElementos; i++)
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

        public bool[] CondicoesContorno(Fixacao fixacao1, Fixacao fixacaoN)
        {
            bool[] condicoesContorno = new bool[Variaveis.GrausLiberdadeMaximo];

            for (int i = 0; i < Variaveis.GrausLiberdadeMaximo; i++)
            {
                if(i == 0)
                {
                    condicoesContorno[i] = fixacao1.Deslocamento;
                    condicoesContorno[i + 1] = fixacao1.Angulo;
                }
                else if (i == Variaveis.GrausLiberdadeMaximo)
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
