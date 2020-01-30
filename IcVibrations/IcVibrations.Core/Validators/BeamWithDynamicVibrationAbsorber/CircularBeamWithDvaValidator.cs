using IcVibrations.Core.Validators.Beam;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.CalculateWithDynamicVibrationAbsorber;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamWithDynamicVibrationAbsorber
{
    public class CircularBeamWithDvaValidator : AbstractBeamRequestValidator<CircularBeamWithDvaRequestData>
    {
        protected override void AbstractValidate(CircularBeamWithDvaRequestData beamData, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}
