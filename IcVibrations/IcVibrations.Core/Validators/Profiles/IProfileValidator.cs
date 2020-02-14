using IcVibrations.Common.Profiles;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.Profiles
{
    /// <summary>
    /// It's responsible to validate any profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface IProfileValidator<TProfile>
        where TProfile : Profile
    {
        Task<bool> Execute(TProfile profile);
    }
}
