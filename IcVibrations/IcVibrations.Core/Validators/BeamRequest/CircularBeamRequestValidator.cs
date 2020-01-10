using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequest
{
    public class CircularBeamRequestValidator : AbstractBeamRequestValidator<CircularBeamRequestData>
    {
        protected override void ValidateShapeInput(CircularBeamRequestData requestData, OperationResponseBase response)
        {
            if(requestData.Diameter <= 0)
            {
                response.AddError("006", $"Diameter : {requestData.Diameter } must be greater than zero.");
            }
        }
    }
}
