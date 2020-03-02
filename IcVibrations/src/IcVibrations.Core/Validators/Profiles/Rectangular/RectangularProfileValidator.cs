using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.Profiles.Rectangular
{
    public class RectangularProfileValidator : ProfileValidator<RectangularProfile>, IRectangularProfileValidator
    {
        public override Task<bool> Execute(RectangularProfile profile, OperationResponseBase response)
        {
            return Task.FromResult(true);
        }
    }
}
