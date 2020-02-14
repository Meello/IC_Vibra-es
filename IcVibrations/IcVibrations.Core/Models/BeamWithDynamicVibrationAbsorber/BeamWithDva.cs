using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;

namespace IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber
{
    /// <summary>
    /// It represents a beam with dynamic vibration absorber.
    /// </summary>
    public class BeamWithDva<TProfile> : Beam<TProfile>
        where TProfile : Profile, new()
    {
        /// <summary>
        /// Mass of each DVA.
        /// </summary>
        public double[] DvaMasses { get; set; }

        /// <summary>
        /// Hardness of each DVA.
        /// </summary>
        public double[] DvaHardnesses { get; set; }

        /// <summary>
        /// Node position of each DVA.
        /// </summary>
        public uint[] DvaNodePositions { get; set; }
    }
}
