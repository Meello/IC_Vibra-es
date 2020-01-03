using StoneCo.Buy4.School.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.DataContract.Piezoelectric.Calculate
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
