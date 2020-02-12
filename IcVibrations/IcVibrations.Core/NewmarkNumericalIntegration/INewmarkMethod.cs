using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod
    {
        Task<NewmarkMethodResponse> CalculateResponse(NewmarkMethodInput input, OperationResponseBase response);
    }
}
