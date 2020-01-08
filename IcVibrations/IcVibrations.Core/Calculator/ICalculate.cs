using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts.Beam;

namespace IcVibrations.Core.Calculator
{
    public interface ICalculate
    {
        Beam Execute(BeamRequestData requestData);
    }
}
