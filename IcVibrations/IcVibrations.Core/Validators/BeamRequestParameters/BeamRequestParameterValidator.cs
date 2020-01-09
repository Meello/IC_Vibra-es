using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequestParameters
{
    public class BeamRequestParameterValidator : IBeamRequestParameterValidator
    {
        public void ValidateNodes(uint nodes, OperationResponseBase response)
        {
            if (nodes <= 2)
            {
                response.AddError("001", "Nodes should be greatter than or equal 2");
            }
        }

    }
}
