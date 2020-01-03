using IC_Vibrations.Core.Operations;
using IC_Vibrations.Core.Operations.Piezoelectric.Calculate;
using IC_Vibrations.DataContract.Beam.CalculatePiezoelectric;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracoes.Core.Operations.Beam.Calculate
{
    public class CalculateBeamVibration : OperationBase<CalculateBeamRequest, CalculateBeamResponse>, ICalculateBeamVibration
    {
        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest request)
        {
            throw new NotImplementedException();
        }

        protected override CalculateBeamResponse ValidateOperation(CalculateBeamRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
