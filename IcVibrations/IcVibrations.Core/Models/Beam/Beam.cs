using IcVibrations.Common.Profiles;

namespace IcVibrations.Core.Models.Beam
{
    /// <summary>
    /// It represents the analyzed beam.
    /// </summary>
    public class Beam<TProfile> : AbstractBeam<TProfile>
        where TProfile : Profile, new()
    {
    }
}
