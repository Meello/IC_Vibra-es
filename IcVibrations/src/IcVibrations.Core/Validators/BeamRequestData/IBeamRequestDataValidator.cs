using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.CalculateVibration;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.BeamRequestData
{
    /// <summary>
    /// It's responsible to validate the beam request.
    /// </summary>
    /// <typeparam name="TBeamData"></typeparam>
    /// <typeparam name="TProfile"></typeparam>
    public interface IBeamRequestDataValidator<TBeamData, TProfile>
        where TProfile : Profile, new()
        where TBeamData : IBeamRequestData<TProfile>
    {
        /// <summary>
        /// Validates the beam request data.
        /// </summary>
        /// <param name="beamData"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        Task<bool> ValidateBeamData(TBeamData beamData, OperationResponseBase response);
    }
}
