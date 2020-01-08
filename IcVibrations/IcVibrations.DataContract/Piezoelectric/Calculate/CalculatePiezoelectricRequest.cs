using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric.Calculate
{
    public class CalculatePiezoelectricRequest : OperationRequestBase
    {
        public CalculatePiezoelectricRequest(PiezoelectricRequestData requestData)
        {
            Data = requestData;
        }

        public PiezoelectricRequestData Data { get; }
    }
}
