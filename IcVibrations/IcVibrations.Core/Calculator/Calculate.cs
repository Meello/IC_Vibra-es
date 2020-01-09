using IcVibrations.Core.Calculator.Variables;
using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Core.DTO;

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

        public MassHardnessDamping Execute(BeamRequestData requestData)
        {
            return null;
        }
    }
}
