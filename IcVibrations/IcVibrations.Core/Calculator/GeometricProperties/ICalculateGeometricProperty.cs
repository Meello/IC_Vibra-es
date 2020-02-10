﻿using System.Threading.Tasks;

namespace IcVibrations.Calculator.GeometricProperties
{
    /// <summary>
    /// It's responsible to calculate any geometric property.
    /// </summary>
    public interface ICalculateGeometricProperty
    {
        /// <summary>
        /// Method to calculate the area to circular profile.
        /// </summary>
        /// <param name="diameter"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        Task<double> Area(double diameter, double thickness);

        /// <summary>
        /// Method to calculate the area to rectangular or square profile.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        Task<double> Area(double height, double width, double thickness);

        /// <summary>
        /// Method to calculate the moment of inertia to circular profile.
        /// </summary>
        /// <param name="diameter"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        Task<double> MomentOfInertia(double diameter, double thickness);

        /// <summary>
        /// Method to calculate the moment of inertia to rectangular or square profile.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        Task<double> MomentOfInertia(double height, double width, double thickness);

        //double StaticalMomentArea(double diameter, double thickness);
    }
}
