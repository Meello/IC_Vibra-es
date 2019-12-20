using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao
{
    public abstract class Apoio
    {
        public abstract bool UsaEixoX { get; }

        public abstract bool UsaEixoY { get; }

        public abstract bool UsaEixoZ { get; }
    }

    public class Engaste : Apoio
    {
        public override bool UsaEixoX => false;

        public override bool UsaEixoY => false;

        public override bool UsaEixoZ => false;
    }

    public class Rotula : Apoio
    {
        public override bool UsaEixoX => true;

        public override bool UsaEixoY => true;

        public override bool UsaEixoZ => true;
    }

    public class Pino : Apoio
    {
        public override bool UsaEixoX => false;

        public override bool UsaEixoY => false;

        public override bool UsaEixoZ => true;
    }
}
