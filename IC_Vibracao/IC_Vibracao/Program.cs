using System;
using static IC_Vibracao.DadosIniciais.Constantes;

namespace IC_Vibracao
{
    class Program
    {
        static void Main(string[] args)
        {
            Barra barra = new Barra();
            barra.Material = new Aco1020();
            barra.Perfil = new PerfilCircular
            {
                Diametro = 0.0254,
                Espessura = 0.0021
            };
            barra.Apoio0 = new Engaste();
            barra.ApoioN = new Engaste();
            barra.QuantElementos = 5;

            //Compilador ignora o _ em números, ajuda a separar os milhares, milhoes, etc
            int[] forcaExterna = ForcaExterna(3,10_000,3);
        }

        public static int[] ForcaExterna(int pontoBarra, int forca, int posicaoBarra)
        {
            return new int[] { };
        }
    }
}
