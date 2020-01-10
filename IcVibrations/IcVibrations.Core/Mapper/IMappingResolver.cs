using IcVibrations.Core.DTO;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        Beam AddValues(CircularBeamRequestData circularBeamRequestData);

        Beam AddValues(RectangularBeamRequestData rectangularBeamRequestData);

        void AddValues(BeamMatrix beamMatrix, Beam beam);
    }
}
