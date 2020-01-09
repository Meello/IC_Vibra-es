using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Calculator.Variables
{
    public interface IVariable
    {
        int Elements(int nodes);

        int DegreesFreedomMaximum(int nodes);
    }
}
