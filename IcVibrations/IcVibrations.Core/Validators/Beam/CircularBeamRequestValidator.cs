using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IcVibrations.Core.Validators.Beam
{
    public class CircularBeamRequestValidator : AbstractBeamRequestValidator<CircularBeamRequestData>
    {
        protected override void ValidateProfileInput(CircularBeamRequestData requestData, OperationResponseBase response)
        {
            if (requestData.Thickness > requestData.Diameter/2)
            {
                response.AddError("023", $"Thickness: {requestData.Thickness} must be smaller than radius: {requestData.Diameter/2}.");
            }

            if (requestData.Diameter <= 0)
            {
                response.AddError("006", $"Diameter : {requestData.Diameter } must be greater than zero.");
            }
        }
    }
}
