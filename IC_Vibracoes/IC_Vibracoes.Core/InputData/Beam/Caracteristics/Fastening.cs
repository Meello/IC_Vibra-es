using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Beam.Properties
{
    public abstract class Fastening
    {
        public abstract bool Deslocamento { get; }

        public abstract bool Angulo { get; }

    }

    public class Collet : Fastening
    {
        public override bool Deslocamento => false;

        public override bool Angulo => false;

    }

    public class Support : Fastening
    {
        public override bool Deslocamento => true;

        public override bool Angulo => true;

    }

    public class Bolt : Fastening
    {
        public override bool Deslocamento => false;

        public override bool Angulo => true;

    }
}
