using IcVibrations.Common;
using IcVibrations.Common.Profiles;
using System.Collections.Generic;

namespace IcVibrations.DataContracts.Beam
{
    /// <summary>
    /// It represents the 'data' content of beam request operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class BeamRequestData<TProfile> 
        where TProfile : Profile
    {
        /// <summary>
        /// Number of elements in the beam.
        /// </summary>
        public uint NumberOfElements { get; set; }

        /// <summary>
        /// Beam material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// beam fastenings.
        /// </summary>
        public List<string> Fastenings { get; set; }

        /// <summary>
        /// Beam length.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Applied forces in the beam.
        /// </summary>
        public List<Force> Forces { get; set; }

        /// <summary>
        /// Beam profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}