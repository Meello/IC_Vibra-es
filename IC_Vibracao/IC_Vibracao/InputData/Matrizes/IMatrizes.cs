using IC_Vibracao.InputData.Barra.Propriedades;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public interface IMatrizes
    {
        int[,] CoordenadaElementos();

        double[] Area(double area);

        double[] MomentoInercia(double momentoInercia);

        double[] ModuloElasticade(double moduloElasticidade);

        double[] Forcamento(double forca, int posicaoForca);

        bool[] CondicoesContorno(Fixacao fixacao1, Fixacao fixacaoN);
    }
}
