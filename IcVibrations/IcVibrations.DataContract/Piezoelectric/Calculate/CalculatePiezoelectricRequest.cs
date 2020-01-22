using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric.Calculate
{
    public class CalculatePiezoelectricRequest<TPiezoelectric,TBeam> : OperationRequestBase
        where TPiezoelectric: PiezoelectricRequestData
        where TBeam: BeamRequestData
    {
        public CalculatePiezoelectricRequest(TPiezoelectric piezoelectricRequestData, NewmarkMethodParameter methodParameterData, TBeam beamRequestData)
        {
            PiezoelectricRequestData = piezoelectricRequestData;
            MethodParameterData = methodParameterData;
            BeamData = beamRequestData;
        }

        public TPiezoelectric PiezoelectricRequestData { get; }

        public NewmarkMethodParameter MethodParameterData { get; }

        public TBeam BeamData { get; }
    }
}
