using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;

namespace IcVibrations.Core.Validators.Beam
{
    public interface IBeamRequestValidator<T> 
        where T : BeamRequestData
    {
        bool Execute(T requestData, uint degreesFreedomMaximum, OperationResponseBase response);
    }
}
