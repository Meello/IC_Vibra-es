using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    public class CalculateBeamRequest<T> : OperationRequestBase 
        where T : BeamRequestData
    {
        public CalculateBeamRequest(T beamData, NewmarkMethodParameter methodParameterData)
        {
            BeamData = beamData;
            MethodParameterData = methodParameterData;
        }

        public T BeamData { get; }

        public NewmarkMethodParameter MethodParameterData { get; }
    }
}
