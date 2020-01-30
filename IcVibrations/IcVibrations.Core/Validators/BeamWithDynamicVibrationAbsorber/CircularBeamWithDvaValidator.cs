using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.DynamicVibrationAbsorber;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IcVibrations.Core.Validators.BeamWithDynamicVibrationAbsorber
{
    public class CircularBeamWithDvaValidator : AbstractBeamRequestValidator<CircularBeamWithDvaRequestData>
    {
        private readonly IDvaValidator _dvaValidator;

        public CircularBeamWithDvaValidator(
            IDvaValidator dvaValidator)
        {
            this._dvaValidator = dvaValidator;
        }

        protected override void AbstractValidate(CircularBeamWithDvaRequestData requestData, OperationResponseBase response)
        {
            this._dvaValidator.Execute(requestData, response);

            if (requestData.Thickness > requestData.Diameter / 2)
            {
                response.AddError("023", $"Thickness: {requestData.Thickness} must be smaller than radius: {requestData.Diameter / 2}.");
            }

            if (requestData.Diameter <= 0)
            {
                response.AddError("006", $"Diameter : {requestData.Diameter } must be greater than zero.");
            }
        }
    }
}
