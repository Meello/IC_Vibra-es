using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Models.Piezoelectric
{
    public class Piezoelectric
    {
        public Profile Profile { get; set; }

        public int[] ElementsWithPiezoelectric { get; set; }
    }
}
