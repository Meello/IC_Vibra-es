using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        Beam AddValues(BeamRequestData circularBeamRequestData);

        OperationResponseData BuildFrom(NewmarkMethodOutput output);
        Beam BuildFrom(CalculateBeamRequest<RectangularBeamRequestData> request);
    }
}
