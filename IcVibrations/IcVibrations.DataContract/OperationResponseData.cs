using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts
{
    public class OperationResponseData
    {
        //public string AnalysisExplanation { get; set; }

        public double[] Time { get; set; }

        public double[] AngularFrequency { get; set; }

        public double[,] YResult { get; set; }

        //public double[,] VelResult { get; set; }

        //public double[,] AcelResult { get; set; }

        public double[,] Force { get; set; }
    }
}
