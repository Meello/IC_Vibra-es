using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.GreaterThanZero
{
    public class GreaterThanZeroValidator : IGreaterThanZeroValidator
    {
        public bool Execute(double? value)
        {
            if(value > 0)
            {
                return true;
            }

            return false;
        }

        public void Execute(double? value, string fieldname, string profile, OperationResponseBase response)
        {
            if(value > 0)
            {
                return;
            }

            response.AddError("003", $"{fieldname}: {value} must be greater than zero to {profile} profile.");
        }

        public void Execute(double? value, string fieldname, OperationResponseBase response)
        {
            if (value > 0)
            {
                return;
            }

            response.AddError("003", $"{fieldname}: {value} must be greater than zero.");
        }
    }
}
