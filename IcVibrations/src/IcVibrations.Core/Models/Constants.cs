﻿namespace IcVibrations.Core.Models
{
    /// <summary>
    /// It contains the constants used in the application.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Nodes per element in a 1D analysis.
        /// OBS.: 1D analysis just calculate the displacement in a single direction, in the case of this project, y axis.
        /// </summary>
        public const int NodesPerElement = 2;

        /// <summary>
        /// Degrees freedom per element.
        /// Calculus: Degrees freedom per element = Nodes Per Element * Degrees Freedom
        /// </summary>
        public const int DegreesFreedomElement = 4;

        /// <summary>
        /// Dimensions in the beam.
        /// OBS.: That is different than analysis dimensions.
        /// </summary>
        public const int Dimensions = 2;

        /// <summary>
        /// Degrees freedom per element.
        /// </summary>
        public const int DegreesFreedom = 2;

        /// <summary>
        /// Constant used in the Newmark numeric integration that indicate the type of integration.
        /// </summary>
        public const double Beta = 0.25;

        /// <summary>
        /// Constant used in the Newmark numeric integration that indicate the type of integration.
        /// </summary>
        public const double Gama = 0.5;

        /// <summary>
        /// Proportionality constant used in the damping calculate. 
        /// That is used with the hardness.
        /// </summary>
        public const double Alpha = 1e-6;

        /// <summary>
        /// Proportionality constant used in the damping calculate. 
        /// That is used with the mass.
        /// </summary>
        public const double Mi = 0;

        /// <summary>
        /// Size of piezoelectric element size.
        /// </summary>
        public const int PiezoelectricElementMatrixSize = 2;
    }
}
