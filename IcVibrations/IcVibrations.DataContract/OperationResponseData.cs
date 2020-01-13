using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts
{
    public class OperationResponseData
    {
        //public string AnalysisExplanation { get; set; }

        public List<double[]> Result { get; set; }

        public List<double> Time { get; set; }
    }
}
