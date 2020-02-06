using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IcVibrations.DataContracts;


namespace IcVibrations.Core.Operations
{
    public interface IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase, new()
    {
        Task<TResponse> Process(TRequest request);
    }
}
