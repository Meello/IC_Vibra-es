using IC_Vibration.InputData.Beam;
using IC_Vibrations.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.Core.Calculus
{
    public interface ICalculus
    {
        Beam Execute(BeamRequestData requestData);
    }
}
