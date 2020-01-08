using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts;


namespace IcVibrations.Core.Operations
{
    public interface IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase, new()
    {
        TResponse Process(TRequest request);
    }
}
