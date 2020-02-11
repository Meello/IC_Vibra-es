using IcVibrations.Common;
using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts.CalculateVibration.Beam;
using System.Collections.Generic;

namespace IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber
{
    /// <summary>
    /// It represents the 'data' content of BeamWithDva request.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class BeamWithDvaRequestData<TProfile> : BeamRequestData<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// List of dynamic vibration absorber.
        /// </summary>
        public List<DynamicVibrationAbsorber> Dvas { get; set; }
    }
}
