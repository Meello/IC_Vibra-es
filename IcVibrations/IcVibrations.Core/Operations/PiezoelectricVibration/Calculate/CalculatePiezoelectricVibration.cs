using IcVibrations.DataContracts.Piezoelectric.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Piezoelectric.Calculate
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
