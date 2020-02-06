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
        /// The analysis results for all angular frequency analyzed.
        /// </summary>
        public List<AnalysisResults> AnalysisResults { get; set; }
    }

    /// <summary>
    /// It represents the analysis result for each angular frequency analyzed.
    /// </summary>
    public class AnalysisResults
    {
        /// <summary>
        /// The angular frequency analyzed.
        /// </summary>
        public double AngularFrequency { get; set; }

        /// <summary>
        /// Time, displacement, velocity, aceleration and force for each node in the analyzed beam.
        /// </summary>
        public List<Result> Results { get; set; }
    }

    /// <summary>
    /// It represents the result for each time iteration.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Iteration time.
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Iteration displacements (linear and angular) for each node.
        /// </summary>
        public double[] Displacemens { get; set; }

        /// <summary>
        /// Force iteration for each node.
        /// </summary>
        public double[] Forces { get; set; }
    }
}
