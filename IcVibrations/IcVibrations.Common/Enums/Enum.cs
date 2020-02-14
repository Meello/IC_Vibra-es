using System;

namespace IcVibrations.Common
{
    public class Enum
    {
        [Flags]
        public enum Fastenings
        {
            None,
            Pinned,
            Fixed,
        }

        [Flags]
        public enum Materials
        {
            Steel1020,
            Steel4130,
            Aluminum
        }

        [Flags]
        public enum Profiles
        {
            Circular,
            Rectangular,
        }
    }
}
