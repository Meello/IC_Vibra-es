using IcVibrations.Common.Profiles;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.Profiles.Rectangular
{
    public class RectangularProfileValidator : ProfileValidator<RectangularProfile>, IRectangularProfileValidator
    {
        public override Task<bool> Execute(RectangularProfile profile)
        {
            return Task.FromResult(true);
        }
    }
}
