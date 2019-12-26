using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Barra.Propriedades
{
    public abstract class Fixacao
    {
        public abstract bool Deslocamento { get; }

        public abstract bool Angulo { get; }

    }

    public class Engaste : Fixacao
    {
        public override bool Deslocamento => false;

        public override bool Angulo => false;

    }

    public class Apoio : Fixacao
    {
        public override bool Deslocamento => true;

        public override bool Angulo => true;

    }

    public class Pino : Fixacao
    {
        public override bool Deslocamento => false;

        public override bool Angulo => true;

    }
}
