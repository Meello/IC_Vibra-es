using IcVibrations.Common.Classes;
using IcVibrations.Core.Models;
using System.Collections.Generic;

namespace IcVibrations.Core.DTO
{
    /// <summary>
    /// It represents the 
    /// </summary>
    public class NewmarkMethodResponse
    {
        /// <summary>
        /// Time, displacement, velocity, aceleration and force for each node in the analyzed beam.
        /// </summary>
        public List<Analysis> Analyses { get; set; }
    }
}
