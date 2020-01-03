using IC_Vibrations.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibrations.Core.Calculus
{
    public interface ICalculus
    {
        void Execute(BeamRequestData requestData);
    }
}
