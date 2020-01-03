using IC_Vibrations.Core.Operations;
using IC_Vibrations.Core.Validators.Beans;
using IC_Vibrations.DataContract.Beam.CalculatePiezoelectric;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracoes.Core.Operations.Beam.Calculate
{
    public class CalculateBeamVibration : OperationBase<CalculateBeamRequest, CalculateBeamResponse>, ICalculateBeamVibration
    {
        private readonly IBeamValidator _beamValidator;

        public CalculateBeamVibration(IBeamValidator beamValidator)
        {
            this._beamValidator = beamValidator;
        }

        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();



            return response;
        }

        protected override CalculateBeamResponse ValidateOperation(CalculateBeamRequest request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            this._beamValidator.Execute();

            return response;
        }
    }
}
