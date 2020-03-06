using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts.CalculateVibration;

namespace IcVibrations.Core.Validators.BeamRequestData
{
    public interface IBeamRequestDataValidator<TBeamData, TProfile>
        where TProfile : Profile, new()
        where TBeamData : IBeamRequestData<TProfile>
    {
    }
}
