using System;
using static IcVibrations.Common.Enum;

namespace IcVibrations.Models.Beam.Characteristics
{
    /// <summary>
    /// It represents the degrees of freedom for a generic fastening.
    /// </summary>
    public abstract class Fastening
    {
        public abstract bool Displacement { get; }

        public abstract bool Angle { get; }

    }

    /// <summary>
    /// It represents the degrees of freedom for a fixed type fastening.
    /// </summary>
    public class Fixed : Fastening
    {
        public override bool Displacement => false;

        public override bool Angle => false;

    }

    /// <summary>
    /// It represents the degrees of freedom for a pinned type fastening.
    /// </summary>
    public class Pinned : Fastening
    {
        public override bool Displacement => false;

        public override bool Angle => true;

    }

    /// <summary>
    /// It represents the degrees of freedom for a case without fastening.
    /// </summary>
    public class None : Fastening
    {
        public override bool Displacement => true;

        public override bool Angle => true;

    }

    /// <summary>
    /// It's responsible to create a fastening object based on a string.
    /// </summary>
    public class FasteningFactory
    {
        public static Fastening Create(string fastening)
        {
            switch ((Fastenings)Enum.Parse(typeof(Fastenings), fastening, ignoreCase: true))
            {
                case Fastenings.Fixed: return new Fixed();
                case Fastenings.Pinned: return new Pinned();
                case Fastenings.None: return new None();
                default: break;
            }

            throw new Exception($"Invalid fastening: {fastening}.");
        }
    }
}
