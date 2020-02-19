using IcVibrations.Core.DTO;
using IcVibrations.Core.DTO.Input;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod
    {
        Task<NewmarkMethodResponse> CalculateResponse(NewmarkMethodInput input, OperationResponseBase response);
    }
}
