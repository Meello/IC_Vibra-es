using IcVibrations.Common.Classes;

namespace IcVibrations.Core.DTO.Input
{
    public interface INewmarkMethodInput
    {
        /// <summary>
        /// Mass matrix of the object that is analyzed.
        /// </summary>
        double[,] Mass { get; set; }

        /// <summary>
        /// Hardness matrix of the object that is analyzed.
        /// </summary>
        double[,] Hardness { get; set; }

        /// <summary>
        /// Damping matrix of the object that is analyzed.
        /// </summary>
        double[,] Damping { get; set; }

        /// <summary>
        /// Force vector of the object that is analyzed.
        /// </summary>
        double[] Force { get; set; }

        /// <summary>
        /// Delta time.
        /// </summary>
        double DeltaTime { get; set; }

        /// <summary>
        /// Angular frequency used in the analysis.
        /// </summary>
        double AngularFrequency { get; set; }

        /// <summary>
        /// Number of boundary conditions that is true.
        /// </summary>
        public uint NumberOfTrueBoundaryConditions { get; set; }

        /// <summary>
        /// Newmark method parameters.
        /// </summary>
        NewmarkMethodParameter Parameter { get; set; }
    }
}
