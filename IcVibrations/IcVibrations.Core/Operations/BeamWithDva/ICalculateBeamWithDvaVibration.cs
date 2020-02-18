using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber;

namespace IcVibrations.Core.Operations.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorber.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateBeamWithDvaVibration<TProfile> : ICalculateVibration<CalculateBeamWithDvaVibrationRequest<TProfile>, BeamWithDvaRequestData<TProfile>, TProfile, BeamWithDva<TProfile>, NewmarkMethodBeamInput>
        where TProfile : Profile, new()
    {
    }
}
