using IcVibrations.Core.Models;
using System.Collections.Generic;

namespace IcVibrations.Core.DTO
{
    /// <summary>
    /// It represents the 
    /// </summary>
    public class NewmarkMethodOutput
    {
        /// <summary>
        /// The angular frequency analyzed.
        /// </summary>
        public double AngularFrequency { get; set; }

        /// <summary>
        /// Time, displacement, velocity, aceleration and force for each node in the analyzed beam.
        /// </summary>
        public List<IterationResult> IterationsResult { get; set; }
    }

    public class IterationResult
    {
        /// <summary>
        /// Iteration time.
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Iteration displacement (linear and angular) for each node.
        /// </summary>
        public double[] Displacemens { get; set; }

        /// <summary>
        /// Iteration velocity (linear and angular) for each node.
        /// </summary>
        public double[] Velocities { get; set; }

        /// <summary>
        /// Iteration aceleration (linear and angular) for each node.
        /// </summary>
        public double[] Acelerations { get; set; }

        /// <summary>
        /// Force iteration for each node.
        /// </summary>
        public double[] Forces { get; set; }
    }
}
