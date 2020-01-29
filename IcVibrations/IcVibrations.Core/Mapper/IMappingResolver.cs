using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        CircularBeam BuildFrom(CircularBeamRequestData circularBeamRequestData);

        RectangularBeam BuildFrom(RectangularBeamRequestData rectangularBeamRequestData);

        RectangularPiezoelectric BuildFrom(PiezoelectricRequestData piezoelectricRequestData);

        OperationResponseData BuildFrom(NewmarkMethodOutput output);
    }
}
