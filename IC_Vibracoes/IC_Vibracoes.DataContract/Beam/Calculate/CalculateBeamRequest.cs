using StoneCo.Buy4.School.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.DataContract.Beam.CalculatePiezoelectric
{
    public class CalculateBeamRequest : OperationRequestBase
    {
        public CalculateBeamRequest(BeamRequestData requestData)
        {
            Data = requestData;
        }

        public BeamRequestData Data { get; set; }
    }
}
