using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        Beam BuildFrom(BeamRequestData circularBeamRequestData);

        OperationResponseData BuildFrom(NewmarkMethodOutput output);
    }
}
