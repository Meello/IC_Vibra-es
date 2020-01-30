using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Beam.CalculateWithDynamicVibrationAbsorber
{
    public class CalculateWithDvaRequest : CalculateBeamRequest<BeamWithDvaRequestData>
    {
        public CalculateWithDvaRequest(BeamWithDvaRequestData beamData, NewmarkMethodParameter methodParameterData) : base(beamData, methodParameterData)
        {
        }
    }
}
