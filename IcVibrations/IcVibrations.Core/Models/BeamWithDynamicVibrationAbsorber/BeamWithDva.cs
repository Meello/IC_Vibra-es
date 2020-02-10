namespace IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber
{
    /// <summary>
    /// It represents a beam with DVA
    /// </summary>
    public class BeamWithDva : Models.Beam.Beam
    {
        /// <summary>
        /// Mass of each DVA.
        /// </summary>
        public double[] DvaMasses { get; set; }

        /// <summary>
        /// Hardness of each DVA.
        /// </summary>
        public double[] DvaHardnesses { get; set; }

        /// <summary>
        /// Node position of each DVA.
        /// </summary>
        public uint[] DvaNodePositions { get; set; }
    }
}
