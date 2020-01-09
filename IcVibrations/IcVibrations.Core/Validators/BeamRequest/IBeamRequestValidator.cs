using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;

namespace IcVibrations.Core.Validators.BeamRequest
{
    public interface IBeamRequestValidator
    {
        bool Execute(BeamRequestData requestDat, OperationResponseBase response);
    }
}
