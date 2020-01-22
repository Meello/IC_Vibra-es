using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.MethodParameters
{
    public interface IMethodParameterValidator
    {
        bool Execute(NewmarkMethodParameter methodParameters, OperationResponseBase response);
    }
}
