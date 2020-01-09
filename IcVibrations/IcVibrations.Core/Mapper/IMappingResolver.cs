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
        ProfileDimension BuildFrom(BeamRequestData requestData);
    }
}
