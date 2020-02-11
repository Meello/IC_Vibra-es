using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts.CalculateVibration;

namespace IcVibrations.Core.Operations
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateVibration<TRequest, TRequestData, TProfile, TBeam> : IOperationBase<TRequest, CalculateVibrationResponse>
        where TProfile : Profile, new()
        where TRequestData : CalculateVibrationRequestData<TProfile>
        where TRequest : CalculateVibrationRequest<TProfile, TRequestData>
        where TBeam : IBeam<TProfile>, new()
    {
    }
}
