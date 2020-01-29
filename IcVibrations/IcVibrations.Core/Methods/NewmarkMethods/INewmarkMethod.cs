using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod
    {
        NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, Beam beam, uint degreesFreedomMaximum);

        NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, RectangularBeam beam, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum);

        NewmarkMethodOutput CreateOutput(NewmarkMethodInput input, OperationResponseBase response);
    }
}
