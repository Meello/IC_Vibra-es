using IcVibrations.Core.DTO.Input;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.Validators.NumericalIntegrationMethods.Newmark
{
    /// <summary>
    /// It's responsible to validate the parameters used in NewmarkMethod class.
    /// </summary>
    public interface INewmarkMethodValidator
    {
        /// <summary>
        /// Validate the parameters used in the method CalculateResponse that weren't validated previously.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public Task<bool> ValidateParameters(NewmarkMethodInput input, OperationResponseBase response);
    }
}
