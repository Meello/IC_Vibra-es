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
        public CalculatePiezoelectricRequest(TBeam beamData, TPiezoelectric piezoelectricRequestData, NewmarkMethodParameter methodParameterData)
        {
            BeamData = beamData;
            PiezoelectricRequestData = piezoelectricRequestData;
            MethodParameterData = methodParameterData;
        }

        public TBeam BeamData { get; }

        public TPiezoelectric PiezoelectricRequestData { get; }

        public NewmarkMethodParameter MethodParameterData { get; }
    }
}
