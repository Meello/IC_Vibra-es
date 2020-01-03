using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibration.InputData.Beam.Characteristics
{
    public abstract class Fastening
    {
        public abstract bool Displacement { get; }

        public abstract bool Angle { get; }

    }

    public class Collet : Fastening
    {
        public override bool Displacement => false;

        public override bool Angle => false;

    }

    public class Support : Fastening
    {
        public override bool Displacement => true;

        public override bool Angle => true;

    }

    public class Bolt : Fastening
    {
        public override bool Displacement => false;

        public override bool Angle => true;

    }
}
