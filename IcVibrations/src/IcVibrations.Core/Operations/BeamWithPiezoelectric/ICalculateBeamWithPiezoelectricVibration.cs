using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts.CalculateVibration.BeamWithPiezoelectric;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with piezoelectric.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateBeamWithPiezoelectricVibration<TProfile> : ICalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<TProfile>, PiezoelectricRequestData<TProfile>, TProfile, BeamWithPiezoelectric<TProfile>>
        where TProfile : Profile, new()
    {
    }
}