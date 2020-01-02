using IC_Vibracao.InputData.Beam.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public interface IMatrizes
    {
        int[,] CoordenadaElementos();

        double[] Area(double area, int n);

        double[] MomentoInercia(double momentoInercia, int n);

        double[] ModuloElasticade(double moduloElasticidade);

        double[] Forcamento(double forca, int posicaoForca);

        bool[] CondicoesContorno(Fastening fixacao1, Fastening fixacaoN);
    }
}
