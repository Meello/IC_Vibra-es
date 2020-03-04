using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.CalculateVibration.Beam
{
    /// <summary>
    /// It represents the 'data' content of beam request operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class BeamRequestData<TProfile> : CalculateVibrationRequestData<TProfile>
        where TProfile : Profile
    {
    }
}