using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public abstract class Material
    {

        //Eaco = 210e9
        //Eal = 80e9
        public abstract double ModuloElasticidade { get; }


        //Saco = 460e6
        //Sal = 350e6
        public abstract double LimiteEscoamento { get; }
    }

    public class Aco : Material
    {
        public override double ModuloElasticidade => 210e9;

        public override double LimiteEscoamento => 460e6;
    }

    public class Aluminio : Material
    {
        public override double ModuloElasticidade => 80e9;

        public override double LimiteEscoamento => 350e6;
    }
}
