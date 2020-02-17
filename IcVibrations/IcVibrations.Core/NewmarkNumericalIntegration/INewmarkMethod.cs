using IcVibrations.Common.Classes;
using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System.Collections.Generic;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod
    {
        IAsyncEnumerable<Analysis> CalculateResponse(NewmarkMethodInput input, OperationResponseBase response);
    }
}
