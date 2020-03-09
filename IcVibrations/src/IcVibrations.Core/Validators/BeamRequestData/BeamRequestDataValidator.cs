using IcVibrations.Common.ErrorCodes;
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
    public abstract class BeamRequestDataValidator<TBeamData, TProfile> : IBeamRequestDataValidator<TBeamData, TProfile>
        where TProfile : Profile, new()
        where TBeamData : IBeamRequestData<TProfile>
    {
        /// <summary>
        /// Validates the beam request data.
        /// </summary>
        /// <param name="beamData"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public virtual Task<bool> ValidateBeamData(TBeamData beamData, OperationResponseBase response)
        {
            if (beamData.NumberOfElements < 1)
            {
                response.AddError(ErrorCode.BeamRequestData, $"Number of elements: {beamData.NumberOfElements} must be greather than or equals to 1.");
            }

            if (true)
            {

            }

            if (!response.Success)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
