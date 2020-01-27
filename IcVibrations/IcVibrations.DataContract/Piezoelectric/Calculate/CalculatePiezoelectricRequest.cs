using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric.Calculate
{
    public class CalculatePiezoelectricRequest: OperationRequestBase
    {
        public CalculatePiezoelectricRequest(RectangularBeamRequestData beamData, PiezoelectricRequestData piezoelectricRequestData, NewmarkMethodParameter methodParameterData)
        {
            BeamData = beamData;
            PiezoelectricRequestData = piezoelectricRequestData;
            MethodParameterData = methodParameterData;
        }

        public RectangularBeamRequestData BeamData { get; }

        public PiezoelectricRequestData PiezoelectricRequestData { get; }

        public NewmarkMethodParameter MethodParameterData { get; }
    }
}
