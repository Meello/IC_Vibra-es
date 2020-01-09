using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BiggerThanZero
{
    public class BiggerThanZeroValidator : IBiggerThanZeroValidator
    {
        public bool Execute(double? value)
        {
            if(value <= 0 || value == null)
            {
                return false;
            }

            return true;
        }
    }
}
