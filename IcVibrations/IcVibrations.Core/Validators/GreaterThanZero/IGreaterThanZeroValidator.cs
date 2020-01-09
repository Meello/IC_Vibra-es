using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.GreaterThanZero
{
    public interface IGreaterThanZeroValidator
    {
        bool Execute(double? value);

        void Execute(double? value, string fieldname, string profile, OperationResponseBase response);

        void Execute(double? value, string fieldname, OperationResponseBase response);
    }
}
