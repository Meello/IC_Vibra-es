using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.Beam
{
    public class RectangularBeamRequestValidator : AbstractBeamRequestValidator<RectangularBeamRequestData>
    {
        protected override void ValidateProfileInput(RectangularBeamRequestData requestData, OperationResponseBase response)
        {
            if(requestData.Thickness > requestData.Width/2)
            {
                response.AddError("021",$"Thickness: {requestData.Thickness} must be smaller than half of width: {requestData.Width}.");
            }

            if (requestData.Thickness > requestData.Height/2)
            {
                response.AddError("022", $"Thickness: {requestData.Thickness} must be smaller than half of height: {requestData.Height}.");
            }

            if (requestData.Width <= 0)
            {
                response.AddError("007",$"Width: {requestData.Width} must be bigger than zero.");
            }
            
            if (requestData.Height <= 0)
            {
                response.AddError("008", $"Height: {requestData.Height} must be bigger than zero.");
            }
        }
    }
}
