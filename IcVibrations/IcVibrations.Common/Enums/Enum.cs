using System;

namespace IcVibrations.Common
{
    public class Enum
    {
        [Flags]
        public enum Fastenings
        {
            Simple = 0,
            Pinned = 1,
            Fixed = 2
        }

        [Flags]
        public enum Materials
        {
            Steal1020 = 0,
            Steal4130 = 1
        }

        [Flags]
        public enum Profiles
        {
            Circular = 0,
            Rectangular = 1
        }
    }
}
