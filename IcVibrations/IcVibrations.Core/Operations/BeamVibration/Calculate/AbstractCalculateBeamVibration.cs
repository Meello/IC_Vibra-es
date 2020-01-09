using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Beam.Calculate
{
    public abstract class AbstractCalculateBeamVibration<T> : OperationBase<CalculateBeamRequest<T>, CalculateBeamResponse> where T : BeamRequestData
    {
        protected abstract double CalculateArea(CalculateBeamRequest<T> request);

        protected abstract double CalculateInertia(CalculateBeamRequest<T> request);

        private readonly IBeamRequestValidator<T> _validator;

        public AbstractCalculateBeamVibration(IBeamRequestValidator<T> validator)
        {
            this._validator = validator;
        }

        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest<T> request)
        {
            var a = this.CalculateArea(request);
            var b = this.CalculateInertia(request);

            return new CalculateBeamResponse();
        }

        protected override CalculateBeamResponse ValidateOperation(CalculateBeamRequest<T> request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();
            
            if (!this._validator.Execute(request.Data, response))
            {
                return response;
            }
            
            return response;
        }
    }
}