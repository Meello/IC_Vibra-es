using IcVibrations.Core.DTO;
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
        NewmarkMethodInput CreateInput(BeamRequestData requestData, Beam beam, int degreesFreedomMaximum);

        NewmarkMethodOutput CreateOutput(NewmarkMethodInput input, OperationResponseBase response);
    }
}
