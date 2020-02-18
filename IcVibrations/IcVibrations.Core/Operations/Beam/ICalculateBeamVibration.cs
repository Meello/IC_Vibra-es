using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts.CalculateVibration.Beam;

namespace IcVibrations.Core.Operations.Beam
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateBeamVibration<TProfile> : ICalculateVibration<CalculateBeamVibrationRequest<TProfile>, BeamRequestData<TProfile>, TProfile, Beam<TProfile>, NewmarkMethodBeamInput>
        where TProfile : Profile, new()
    {
    }
}
