using IcVibrations.Common.Classes;
using IcVibrations.Core.DTO.Input;
using IcVibrations.DataContracts;
using System.Collections.Generic;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod<TNewmarMethod>
        where TNewmarMethod : INewmarkMethodInput
    {
        IAsyncEnumerable<Analysis> CalculateResponse(TNewmarMethod input, OperationResponseBase response);
    }
}
