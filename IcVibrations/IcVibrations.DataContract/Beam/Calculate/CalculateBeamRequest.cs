using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    public class CalculateBeamRequest : OperationRequestBase
    {
        public CalculateBeamRequest(BeamRequestData requestData)
        {
            Data = requestData;
        }

        public BeamRequestData Data { get; }
    }
}
