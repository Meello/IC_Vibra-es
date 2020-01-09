using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Core.DTO;

namespace IcVibrations.Core.Calculator
{
    public interface ICalculate
    {
        MassHardnessDamping Execute(BeamRequestData requestData);
    }
}
