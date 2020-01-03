using System;
using System.Collections.Generic;
using System.Text;
using StoneCo.Buy4.School.DataContracts;


namespace IC_Vibrations.Core.Operations
{
    public interface IOperationBase<TRequest, TResponse>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase, new()
    {
        TResponse Process(TRequest request);
    }
}
