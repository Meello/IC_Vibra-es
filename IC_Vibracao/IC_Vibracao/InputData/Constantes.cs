using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData
{
    public class Constantes
    {
        public class Variaveis
        {
            public const int NumeroNos = 3;

            public const int NumeroNosPorElementos = 2;

            public const int NumeroDimensoes = 2;

            public const int GrausLiberdade = 2;

            public const int NumeroElementos = NumeroNos - 1;

            public const int GrausLiberdadeMaximo = NumeroNos * GrausLiberdade;

            public const int GrausLiberdadeElemenento = GrausLiberdade * NumeroNosPorElementos;
        }
    }
}
