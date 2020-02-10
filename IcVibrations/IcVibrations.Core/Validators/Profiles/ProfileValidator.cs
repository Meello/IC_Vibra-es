using IcVibrations.Common.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.Profiles
{
    /// <summary>
    /// It's responsible to validate any profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class ProfileValidator<TProfile> : IProfileValidator<TProfile>
        where TProfile : Profile
    {
        public abstract bool Execute(TProfile profile);
    }
}
