using IcVibrations.DataContracts;
using System.Threading.Tasks;


namespace IcVibrations.Core.Operations
{
    public interface IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase, new()
    {
        Task<TResponse> Process(TRequest request);
    }
}
