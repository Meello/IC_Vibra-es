using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Beam.Calculate
{
    public class CalculateBeamVibration : OperationBase<CalculateBeamRequest, CalculateBeamResponse>, ICalculateBeamVibration
    {
        private readonly IBeamRequestValidator _validateBeamRequest;

        public CalculateBeamVibration(
            IBeamRequestValidator validateBeamRequest)
        {
            this._validateBeamRequest = validateBeamRequest;
        }

        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            // Operation

            return response;
        }

        protected override CalculateBeamResponse ValidateOperation(CalculateBeamRequest request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            if(!this._validateBeamRequest.Execute(request.Data, response))
            {
                return response;
            }

            return response;
        }
    }
}
