using IcVibrations.Core.Calculator.Variables;
using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts.Beam;

namespace IcVibrations.Core.Calculator
{
    public class Calculate : ICalculate
    {
        private readonly IVariable _variable;
        private readonly IMainMatrix _principalMatrix;
        private readonly IGeometricProperties _geometricProperties;

        public Calculate(
            IVariable variable,
            IMainMatrix principalMatrix,
            IGeometricProperties geometricProperties)
        {
            this._variable = variable;
            this._principalMatrix = principalMatrix;
            this._geometricProperties = geometricProperties;
        }

        public Beam Execute(BeamRequestData requestData)
        {
            Beam beam = new Beam();
            int dfMax = this._variable.DegreesFreedomMaximum(requestData.Nodes, requestData.DegreesFreedom);
            int dfEl = this._variable.DegreesFreedomPerElemenent(requestData.DegreesFreedom, requestData.NodesPerElement);
            int el = this._variable.Elements(requestData.Nodes);
            double area;
            double[] areaMatrix;
            
            if(requestData.Profile == "Rectangle")
            {
                area = this._geometricProperties.Area(requestData.Diameter, requestData.Thickness);
            }
            else if (requestData.Profile == "Circular")
            {
                area = this._geometricProperties.Area(requestData.Height,requestData.Width, requestData.Thickness);
            }

            return beam;
        }
    }

    public enum algo
    {
        Retangular = 1,
        Circular = 2
    }
}
