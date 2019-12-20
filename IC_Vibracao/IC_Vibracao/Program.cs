using System;

namespace IC_Vibracao
{
    class Program
    {
        static void Main(string[] args)
        {
            Barra barra = new Barra();
            barra.Material = new Aco();
            barra.Perfil = new PerfilCircular
            {
                Diametro = 0.0254,
                Parede = 0.0021
            };
            barra.Apoio0 = new Engaste();
            barra.ApoioN = new Engaste();
            barra.QuantElementos = 5;

            //Compilador ignora o _ em números, ajuda a separar os milhares, milhoes, etc
            int[] forcaExterna = ForcaExterna(barra,10_000,3);

            Console.WriteLine("Hello World!");
        }

        public static int[] ForcaExterna(Barra barra, int forca, int posicaoBarra)
        {
            return new int[] { };
        }
    }
}
