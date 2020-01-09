using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Beam.Calculate
{
    public abstract class AbstractCalculateBeamVibration : OperationBase<CalculateBeamRequest, CalculateBeamResponse>, ICalculateBeamVibration
    {
        protected abstract double CalculateArea(BeamRequestData requestData);

        protected abstract double CalculateInertia(BeamRequestData requestData);

        private readonly IBeamRequestValidator _validateBeamRequest;

        public AbstractCalculateBeamVibration(
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