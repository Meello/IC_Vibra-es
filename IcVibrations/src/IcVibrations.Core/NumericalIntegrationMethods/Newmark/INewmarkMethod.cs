using IcVibrations.Core.DTO.Input;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.NumericalIntegrationMethods.Newmark
{
    /// <summary>
    /// It's responsible to execute the Newmark numerical integration method to calculate the vibration.
    /// </summary>
    public interface INewmarkMethod
    {
        /// <summary>
        /// Calculates and write in a file the response matrixes.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        Task CalculateResponse(NewmarkMethodInput input, OperationResponseBase response);
    }
}
