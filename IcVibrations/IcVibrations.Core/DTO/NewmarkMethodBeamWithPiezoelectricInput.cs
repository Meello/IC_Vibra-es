namespace IcVibrations.Core.DTO
{
    public class NewmarkMethodBeamWithPiezoelectricInput : NewmarkMethodInput
    {
        /// <summary>
        /// Number of boundary conditions that is true in the beam.
        /// </summary>
        public uint NumberOfTrueBeamBoundaryConditions { get; set; }

        /// <summary>
        /// Number of boundary conditions that is true in the piezoelectric.
        /// </summary>
        public uint NumberOfTruePiezoelectricBoundaryConditions { get; set; }
    }
}
