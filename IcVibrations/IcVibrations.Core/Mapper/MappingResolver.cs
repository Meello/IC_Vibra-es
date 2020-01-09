using IcVibrations.Core.DTO;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Mapper
{
    public class MappingResolver : IMappingResolver
    {
        public ProfileDimension BuildFrom(BeamRequestData requestData)
        {
            if(requestData == null)
            {
                return null;
            }

            return new ProfileDimension
            {
                Diameter = requestData.Diameter,
                Height = requestData.Height,
                Width = requestData.Width
            };
        }
    }
}
