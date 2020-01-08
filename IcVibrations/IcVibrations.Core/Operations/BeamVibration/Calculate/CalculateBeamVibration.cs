using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.Beans;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Beam.Calculate
{
    public class CalculateBeamVibration : OperationBase<CalculateBeamRequest, CalculateBeamResponse>, ICalculateBeamVibration
    {
        private readonly IValidateBeam _beamValidator;

        public CalculateBeamVibration(
            IValidateBeam beamValidator)
        {
            this._beamValidator = beamValidator;
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

            this._beamValidator.Execute(request.Data);

            return response;
        }
    }
}
