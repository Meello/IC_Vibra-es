using IcVibrations.Common.Profiles;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.Profiles
{
    /// <summary>
    /// It's responsible to validate any profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class ProfileValidator<TProfile> : IProfileValidator<TProfile>
        where TProfile : Profile
    {
        public abstract Task<bool> Execute(TProfile profile);
    }
}
