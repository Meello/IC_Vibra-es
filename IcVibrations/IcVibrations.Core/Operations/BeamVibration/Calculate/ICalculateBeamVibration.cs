using IcVibrations.Core.Operations;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Beam.Calculate
{
    public interface ICalculateBeamVibration : IOperationBase<CalculateBeamRequest, CalculateBeamResponse>
    {
    }
}
