using IcVibrations.DataContracts.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts.Piezoelectric
{
    public class CalculatePiezoelectricRequestData
    {
        public OperationRequestBaseData MethodParameters { get; set; }

        public BeamRequestData BeamRequestData { get; set; }

        public PiezoelectricRequestData PiezoelectricRequestData { get; set; }
    }
}
