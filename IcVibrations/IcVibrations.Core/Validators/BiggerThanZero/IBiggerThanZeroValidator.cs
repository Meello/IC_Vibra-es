using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BiggerThanZero
{
    public interface IBiggerThanZeroValidator
    {
        bool Execute(double? value);
    }
}
