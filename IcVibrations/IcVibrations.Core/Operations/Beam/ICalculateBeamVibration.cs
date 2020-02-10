using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts.Beam.Calculate;

namespace IcVibrations.Core.Operations.BeamVibration
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateBeamVibration<TProfile> : IOperationBase<CalculateBeamVibrationRequest<TProfile>, CalculateBeamVibrationResponse>
        where TProfile : Profile
    {
    }
}
