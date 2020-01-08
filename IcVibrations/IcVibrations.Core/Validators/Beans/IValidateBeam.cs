using System;
using System.Collections.Generic;
using System.Text;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;

namespace IcVibrations.Core.Validators.Beans
{
    public interface IValidateBeam
    {
        void Execute(BeamRequestData requestData);
    }
}
