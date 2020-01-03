using IC_Vibrations.Core.Operations;
using IC_Vibrations.DataContract.Beam.CalculatePiezoelectric;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracoes.Core.Operations.Beam.Calculate
{
    public interface ICalculateBeamVibration: IOperationBase<CalculateBeamRequest, CalculateBeamResponse>
    {
    }
}
