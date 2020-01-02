using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Barra.Propriedades
{
    public abstract class Material
    {
        // Unidade: Pa
        public abstract double ModuloElasticidade { get; }

        // Unidade: Pa
        public abstract double LimiteEscoamento { get; }

        // Unidade: kg/m³
        public abstract double Densidade { get; }
    }

    public class Aco1020 : Material
    {
        public override double ModuloElasticidade => 205e9;

        public override double LimiteEscoamento => 350e6;

        public override double Densidade => 7850;
    }

    public class Aco4130 : Material
    {
        public override double ModuloElasticidade => 200e9;

        public override double LimiteEscoamento => 460e6;

        public override double Densidade => 7850;
    }

    public class Aluminio : Material
    {
        public override double ModuloElasticidade => 70e9;

        public override double LimiteEscoamento => 300e6;

        public override double Densidade => 2700;
    }
}
