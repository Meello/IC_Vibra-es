using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric.Calculate
{
    public class CalculatePiezoelectricRequest : OperationRequestBase
    {
        public CalculatePiezoelectricRequest(CalculatePiezoelectricRequestData requestData)
        {
            Data = requestData;
        }

        public CalculatePiezoelectricRequestData Data { get; }
    }
}
