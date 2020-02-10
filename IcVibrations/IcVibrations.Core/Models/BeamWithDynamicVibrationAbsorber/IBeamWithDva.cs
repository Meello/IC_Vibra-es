using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;

namespace IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber
{
    /// <summary>
    /// It represents a beam with dynamic vibration absorber.
    /// </summary>
    public interface IBeamWithDva<TProfile> : IBeam<TProfile>
        where TProfile : Profile, new()
    {
        /// <summary>
        /// Mass of each DVA.
        /// </summary>
        double[] DvaMasses { get; set; }

        /// <summary>
        /// Hardness of each DVA.
        /// </summary>
        double[] DvaHardnesses { get; set; }

        /// <summary>
        /// Node position of each DVA.
        /// </summary>
        uint[] DvaNodePositions { get; set; }
    }
}
