using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracoes.Core.Calculus.Variables
{
    public interface IVariable
    {
        int Elements(int nodes);

        int DegreesFreedomMaximum(int nodes, int degreesFreedom);

        int DegreesFreedomPerElemenent(int degreesFreedom, int nodesPerElement);
    }
}
