using IC_Vibracao.InputData.Barra;
using IC_Vibracao.InputData.Barra.Propriedades;
using IC_Vibracao.InputData.Barra.PropriedadesGeometricas;
using System;
using static IC_Vibracao.InputData.Constantes;

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

            barra.Apoio1 = new Engaste();
            barra.ApoioN = new Engaste();

            //Compilador ignora o _ em números, ajuda a separar os milhares, milhoes, etc
            int[] forcaExterna = ForcaExterna(3,10_000,3);
        }

        public static int[] ForcaExterna(int pontoBarra, int forca, int posicaoBarra)
        {
            return new int[] { };
        }
    }
}
