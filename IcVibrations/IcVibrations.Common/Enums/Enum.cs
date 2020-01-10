using System;

namespace IcVibrations.Common
{
    public class Enum
    {
        [Flags]
        public enum Fastenings
        {
            Simple = 1,
            Pinned = 2,
            Fixed = 4
        }

        [Flags]
        public enum Materials
        {
            Steel1020 = 1,
            Steel4130 = 2
        }

        [Flags]
        public enum Profiles
        {
            Circular = 1,
            Rectangular = 2
        }
    }
}
