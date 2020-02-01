using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IcVibrations.Core.Validators.MethodParameters
{
    public class MethodParameterValidator : IMethodParameterValidator
    {
        public bool Execute(NewmarkMethodParameter methodParameters, OperationResponseBase response)
        {
            this.ValidateInitialTime(methodParameters.InitialTime, response);

            this.ValidatePeriodDivision(methodParameters.PeriodDivion, response);

            this.ValidatePeriodCount(methodParameters.PeriodCount, response);

            this.ValidateAngularFrequency(methodParameters.InitialAngularFrequency, methodParameters.DeltaAngularFrequency.Value, methodParameters.FinalAngularFrequency, response);

            if(response.Errors.Count() > 0)
            {
                return false;
            }

            return true;
        }

        private void ValidateInitialTime(double initialTime, OperationResponseBase response)
        {
            if (initialTime < 0)
            {
                response.AddError("014", $"Initial time: {initialTime} must be greater than or equals to zero.");
            }
        }

        private void ValidatePeriodDivision(double periodDivision, OperationResponseBase response)
        {
            if (periodDivision <= 0)
            {
                response.AddError("015", $"Period division: {periodDivision} must be greater than zero.");
            }
        }

        private void ValidatePeriodCount(double periodCount, OperationResponseBase response)
        {
            if (periodCount <= 0)
            {
                response.AddError("016", $"Period count: {periodCount} must be greater than zero.");
            }
        }

        private void ValidateAngularFrequency(double initialAngularFrequency, double deltaAngularFrequency, double finalAngularFrequency, OperationResponseBase response)
        {
            if (initialAngularFrequency < 0)
            {
                response.AddError("017", $"Initial angular frequency: {initialAngularFrequency} must be greater than or equals to zero.");
            }

            if (finalAngularFrequency <= 0)
            {
                response.AddError("019", $"Final angular frequency: {finalAngularFrequency} must be greater than zero.");
            }

            if(deltaAngularFrequency != default)
            {
                if (deltaAngularFrequency > finalAngularFrequency - initialAngularFrequency)
                {
                    response.AddError("020", $"Delta angular frequency: {deltaAngularFrequency} must be smaller than the interval: {finalAngularFrequency - initialAngularFrequency}.");
                }
            }
        }
    }
}
