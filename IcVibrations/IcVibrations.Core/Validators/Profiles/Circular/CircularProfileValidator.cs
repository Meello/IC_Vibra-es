using IcVibrations.Common.Profiles;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.Profiles.Circular
{
    public class CircularProfileValidator : ProfileValidator<CircularProfile>, ICircularProfileValidator
    {
        public override Task<bool> Execute(CircularProfile profile)
        {
            return Task.FromResult(true);
        }
    }
}
