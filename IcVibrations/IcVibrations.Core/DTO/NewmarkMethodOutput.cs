using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.DTO
{
    public class NewmarkMethodOutput
    {
        public List<double[]> Result { get; set; }

        public List<double> Time { get; set; }
    }
}
