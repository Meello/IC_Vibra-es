using IcVibrations.DataContracts.Piezoelectric.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Piezoelectric.Calculate
{
    public interface ICalculatePiezoelectricVibration : IOperationBase<CalculatePiezoelectricRequest, CalculatePiezoelectricResponse>
    {
    }
}
