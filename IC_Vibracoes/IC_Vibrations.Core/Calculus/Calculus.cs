using IC_Vibracoes.Core.Calculus.Variables;
using IC_Vibrations.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.Core.Calculus
{
    public class Calculus : ICalculus
    {
        private readonly IVariable _variable;

        public Calculus(
            IVariable variable)
        {
            this._variable = variable;
        }

        public void Execute(BeamRequestData requestData)
        {
            int dfMax = this._variable.DegreesFreedomMaximum(requestData.Nodes, requestData.DegreesFreedom);
            int dfEl = this._variable.DegreesFreedomPerElemenent(requestData.DegreesFreedom, requestData.NodesPerElement);
            int el = this._variable.Elements(requestData.Nodes);


        }
    }
}
