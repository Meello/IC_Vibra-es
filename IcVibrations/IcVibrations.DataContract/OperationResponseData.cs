using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.DataContracts
{
    /// <summary>
    /// It represents the 'data' content of all operation response.
    /// </summary>
    public class OperationResponseData
    {
        /// <summary>
        /// Who made the analysis.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// A simple analysis explanation .
        /// </summary>
        public string AnalysisExplanation { get; set; }

        /// <summary>
        /// The analysis results for each angular frequency analyzed.
        /// </summary>
        public List<AnalysisResult> AnalysisResults { get; set; }
    }

    public class AnalysisResult
    {
        /// <summary>
        /// The angular frequency analyzed.
        /// </summary>
        public double AngularFrequency { get; set; }

        /// <summary>
        /// Time, displacement, velocity, aceleration and force for each node in the analyzed beam.
        /// </summary>
        public List<double> Results { get; set; }
    }
}
