using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Calculator.Variables
{
    public class Variable : IVariable
    {
        public int Elements(int nodes)
        {
            int elements = nodes - 1;
            return elements;
        }

        public int DegreesFreedomMaximum(int nodes, int degreesFreedom)
        {
            int degreesFreedomMaximum = nodes * degreesFreedom;
            return degreesFreedomMaximum;
        }

        public int DegreesFreedomPerElemenent(int degreesFreedom, int nodesPerElement)
        {
            int degreesFreedomPerElemenent = degreesFreedom * nodesPerElement;
            return degreesFreedomPerElemenent;
        }
    }
}
