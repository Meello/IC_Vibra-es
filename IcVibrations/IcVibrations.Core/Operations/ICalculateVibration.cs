using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.Calculate;

namespace IcVibrations.Core.Operations
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateVibration<TRequest, TProfile, TBeam> : IOperationBase<TRequest, CalculateBeamVibrationResponse>
        where TProfile : Profile
        where TRequest : OperationRequestBase, ICalculateBeamVibrationRequest<TProfile>
        where TBeam : IBeam<TProfile>, new()
    {
    }
}
