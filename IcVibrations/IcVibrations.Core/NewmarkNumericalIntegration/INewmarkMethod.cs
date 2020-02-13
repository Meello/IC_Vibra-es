using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod
    {
        Task<NewmarkMethodResponse> CalculateResponse(NewmarkMethodInput input, OperationResponseBase response);
    }
}
