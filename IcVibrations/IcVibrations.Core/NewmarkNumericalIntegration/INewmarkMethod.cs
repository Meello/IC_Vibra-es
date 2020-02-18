using IcVibrations.Core.DTO;
using IcVibrations.Core.DTO.Input;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod<TNewmarMethod>
        where TNewmarMethod : INewmarkMethodInput
    {
        Task<NewmarkMethodResponse> CalculateResponse(TNewmarMethod input, OperationResponseBase response);
    }
}
