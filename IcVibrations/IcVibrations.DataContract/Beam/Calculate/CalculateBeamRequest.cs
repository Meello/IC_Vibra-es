using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    public class CalculateBeamRequest<T> : OperationRequestBase where T : BeamRequestData
    {
        public CalculateBeamRequest(T requestData)
        {
            Data = requestData;
        }

        public T Data { get; }
    }
}
