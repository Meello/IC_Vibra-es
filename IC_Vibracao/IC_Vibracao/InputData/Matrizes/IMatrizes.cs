using IC_Vibracao.InputData.Barra.Propriedades;
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

        bool[] CondicoesContorno(Fixacao fixacao1, Fixacao fixacaoN);
    }
}
