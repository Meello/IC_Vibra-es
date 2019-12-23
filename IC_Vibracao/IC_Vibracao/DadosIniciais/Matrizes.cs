using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.DadosIniciais
{
    public class Matrizes
    {
        public Matrizes()
        {

        }

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


    }
}
