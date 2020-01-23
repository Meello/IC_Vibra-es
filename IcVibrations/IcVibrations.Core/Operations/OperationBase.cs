using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations
{
    public abstract class OperationBase<TRequest, TResponse> : IOperationBase<TRequest, TResponse>
        where TRequest: OperationRequestBase
        where TResponse: OperationResponseBase, new() 
        //faz com que só permita usar uma classe que tenha um construtor padrão
    {
        public TResponse Process(TRequest request)
        {
            TResponse response = new TResponse();

            try
            {
                response = ValidateOperation(request);
                if(!response.Success)
                {
                    return response;
                }

                response = ProcessOperation(request);
            }
            catch (Exception ex)
            {
                response = new TResponse();
                response.AddError("000", $"{ex.Message}");
            }

            return response;
        }

        protected abstract TResponse ProcessOperation(TRequest request);

        protected abstract TResponse ValidateOperation(TRequest request);
    }
}
