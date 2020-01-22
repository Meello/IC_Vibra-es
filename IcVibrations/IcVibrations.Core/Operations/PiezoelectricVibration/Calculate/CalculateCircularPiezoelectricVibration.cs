using IcVibrations.Core.Operations.Piezoelectric.Calculate;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Piezoelectric;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.PiezoelectricVibration.Calculate
{
    public class CalculateCircularPiezoelectricVibration : AbstractCalculatePiezoelectricVibration<CircularPiezoelectricRequestData, CircularBeamRequestData>
    {
        public CalculateCircularPiezoelectricVibration(
            IMethodParameterValidator methodParameterValidator,
            IBeamRequestValidator<CircularBeamRequestData> beamRequestValidator) : base(methodParameterValidator, beamRequestValidator)
        {
        }
    }
}
