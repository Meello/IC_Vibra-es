using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts
{
    public class OperationRequestBaseData
    {
        public int ElementCount { get; set; }

        public double InitialTime { get; set; }

        public int PeriodDivion { get; set; }

        public int PeriodCount { get; set; }

        public double InitialAngularFrequency { get; set; }

        public double DeltaAngularFrequency { get; set; }

        public double FinalAngularFrequency { get; set; }
    }
}
