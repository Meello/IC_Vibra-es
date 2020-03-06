using IcVibrations.Common.ErrorCodes;
using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.CalculateVibration;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.BeamRequestData
{
    public abstract class BeamRequestDataValidator<TBeamData, TProfile> : IBeamRequestDataValidator<TBeamData, TProfile>
        where TProfile : Profile, new()
        where TBeamData : IBeamRequestData<TProfile>
    {
        public virtual Task<bool> ValidateBeamData(TBeamData beamData, OperationResponseBase response)
        {
            if (beamData.NumberOfElements < 1)
            {
                response.AddError(ErrorCode.BeamRequestData, "");
            }
        }
    }
}
