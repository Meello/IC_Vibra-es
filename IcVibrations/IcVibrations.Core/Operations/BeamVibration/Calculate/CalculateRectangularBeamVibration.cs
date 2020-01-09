using IcVibrations.Core.Operations.Beam.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public class CalculateRectangularBeamVibration : AbstractCalculateBeamVibration<RectangularBeamRequestData>
    {
        public CalculateRectangularBeamVibration(IBeamRequestValidator<RectangularBeamRequestData> validator) : base(validator)
        {
        }

        protected override double CalculateArea(CalculateBeamRequest<RectangularBeamRequestData> request)
        {
            return request.Data.Width * request.Data.Height;
        }

        protected override double CalculateInertia(CalculateBeamRequest<RectangularBeamRequestData> request)
        {
            return request.Data.Width * 1.23;
        }
    }
}
