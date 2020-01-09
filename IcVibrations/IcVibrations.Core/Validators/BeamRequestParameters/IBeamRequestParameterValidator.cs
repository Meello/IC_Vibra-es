using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequestParameters
{
    public interface IBeamRequestParameterValidator
    {
        void ValidateNodes(uint nodes, OperationResponseBase response);
    }
}
