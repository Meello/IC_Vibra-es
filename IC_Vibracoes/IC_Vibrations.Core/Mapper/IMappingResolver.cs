using IC_Vibration.InputData.Beam;
using IC_Vibrations.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracoes.Core.Mapper
{
    public interface IMappingResolver
    {
        Beam BuildFrom(BeamRequestData requestData);
    }
}
