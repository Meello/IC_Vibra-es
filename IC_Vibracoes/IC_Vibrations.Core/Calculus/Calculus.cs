using IC_Vibracoes.Core.Calculus.Variables;
using IC_Vibration.Calculus.GeometricProperties;
using IC_Vibration.Calculus.PrincipalMatrixes;
using IC_Vibrations.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.Core.Calculus
{
    public class Calculus : ICalculus
    {
        private readonly IVariable _variable;
        private readonly IPrincipalMatrix _principalMatrix;
        private readonly IGeometricProperties _geometricProperties;

        public Calculus(
            IVariable variable,
            IPrincipalMatrix principalMatrix,
            IGeometricProperties geometricProperties)
        {
            this._variable = variable;
            this._principalMatrix = principalMatrix;
            this._geometricProperties = geometricProperties;
        }

        public void Execute(BeamRequestData requestData)
        {
            int dfMax = this._variable.DegreesFreedomMaximum(requestData.Nodes, requestData.DegreesFreedom);
            int dfEl = this._variable.DegreesFreedomPerElemenent(requestData.DegreesFreedom, requestData.NodesPerElement);
            int el = this._variable.Elements(requestData.Nodes);
            double[] area;

            if(requestData.Profile == "Rectangle")
            {
                area = 
            }
        }
    }

    public enum algo
    {
        Retangular = 1,
        Circular = 2
    }
}
