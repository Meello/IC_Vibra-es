using IC_Vibrations.DataContract.Piezoelectric.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.Core.Operations.Piezoelectric.Calculate
{
    public class CalculatePiezoelectricVibration : OperationBase<CalculatePiezoelectricRequest, CalculatePiezoelectricResponse>, ICalculatePiezoelectricVibration
    {
        protected override CalculatePiezoelectricResponse ProcessOperation(CalculatePiezoelectricRequest request)
        {
            throw new NotImplementedException();
        }

        protected override CalculatePiezoelectricResponse ValidateOperation(CalculatePiezoelectricRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
