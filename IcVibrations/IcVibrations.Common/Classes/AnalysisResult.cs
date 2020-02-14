using System.Collections.Generic;

namespace IcVibrations.Common.Classes
{
    /// <summary>
    /// It represents the analysis result for each angular frequency analyzed.
    /// </summary>
    public class Analysis
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
}   
