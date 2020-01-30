using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber
{
    public class CalculateBeamWithDvaRequest<T> : CalculateBeamRequest<T>
        where T : BeamWithDvaRequestData
    {
        public CalculateBeamWithDvaRequest(T beamData, NewmarkMethodParameter methodParameterData) : base(beamData, methodParameterData)
        {
        }
    }
}
