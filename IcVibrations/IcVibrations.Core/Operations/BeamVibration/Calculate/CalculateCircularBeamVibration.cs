using IcVibrations.Core.Operations.Beam.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public class CalculateCircularBeamVibration : AbstractCalculateBeamVibration<CircularBeamRequestData>
    {
        public CalculateCircularBeamVibration(IBeamRequestValidator<CircularBeamRequestData> validator) : base(validator)
        {
        }

        protected override double CalculateArea(CalculateBeamRequest<CircularBeamRequestData> request)
        {
            return Math.Pow((request.Data.Diameter / 2), 2) * 3.14;
        }

        protected override double CalculateInertia(CalculateBeamRequest<CircularBeamRequestData> request)
        {
            return request.Data.Diameter * 3.21;
        }
    }
}
