using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequest
{
    public class RectangularBeamRequestValidator : AbstractBeamRequestValidator<RectangularBeamRequestData>
    {
        protected override void ValidateShapeInput(RectangularBeamRequestData requestData, OperationResponseBase response)
        {
            if(requestData.Width <= 0)
            {
            }

            return;
        }
    }
}
