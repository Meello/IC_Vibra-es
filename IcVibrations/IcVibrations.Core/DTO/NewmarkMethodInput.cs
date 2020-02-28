﻿using IcVibrations.Common.Classes;

namespace IcVibrations.Core.DTO.Input
{
    public class NewmarkMethodInput
    {
        /// <summary>
        /// Mass matrix of the object that is analyzed.
        /// </summary>
        public double[,] Mass { get; set; }

        /// <summary>
        /// Hardness matrix of the object that is analyzed.
        /// </summary>
        public double[,] Hardness { get; set; }

        /// <summary>
        /// Damping matrix of the object that is analyzed.
        /// </summary>
        public double[,] Damping { get; set; }

        /// <summary>
        /// Force vector of the object that is analyzed.
        /// </summary>
        public double[] Force { get; set; }

        /// <summary>
        /// Delta time.
        /// </summary>
        public double DeltaTime { get; set; }

        /// <summary>
        /// Angular frequency used in the analysis.
        /// </summary>
        public double AngularFrequency { get; set; }

        /// <summary>
        /// Number of boundary conditions that is true.
        /// </summary>
        public uint NumberOfTrueBoundaryConditions { get; set; }

        /// <summary>
        /// Number of dynamic vibration absorbers.
        /// </summary>
        public uint NumberOfDvas { get; set; }

        /// <summary>
        /// Newmark method parameters.
        /// </summary>
        public NewmarkMethodParameter Parameter { get; set; }
    }
}
