using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Calculator.GeometricProperties
{
    /// <summary>
    /// It's responsible to calculate any geometric property.
    /// </summary>
    public class CalculateGeometricProperty : ICalculateGeometricProperty
    {
        /// <summary>
        /// Method to calculate the area to circular profile.
        /// </summary>
        /// <param name="diameter"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public Task<double> Area(double diameter, double thickness)
        {
            double area;

            if (thickness == default)
            {
                area = Math.PI * Math.Pow(diameter, 2) / 4;
            }
            else
            {
                area = (Math.PI / 4) * (Math.Pow(diameter, 2) - Math.Pow(diameter - 2 * thickness, 2));
            }

            return area;
        }
        /// <summary>
        /// Method to calculate the area to rectangular or square profile.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public Task<double> Area(double height, double width, double thickness)
        {
            double area;

            if (thickness == default)
            {
                area = height * width;
            }
            else
            {
                area = (height * width) - ((height - 2 * thickness) * (width - 2 * thickness));
            }

            return area;
        }

        /// <summary>
        /// Method to calculate the moment of inertia to circular profile.
        /// </summary>
        /// <param name="diameter"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public Task<double> MomentOfInertia(double diameter, double thickness)
        {
            double momentoInercia;

            if (thickness == default)
            {
                momentoInercia = Math.PI * Math.Pow(diameter, 4) / 64;
            }
            else
            {
                momentoInercia = (Math.PI / 64) * (Math.Pow(diameter, 4) - Math.Pow(diameter - 2 * thickness, 4));
            }

            return momentoInercia;
        }

        /// <summary>
        /// Method to calculate the moment of inertia to rectangular or square profile.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public Task<double> MomentOfInertia(double height, double width, double thickness)
        {
            double momentoInercia;

            if (thickness == default)
            {
                momentoInercia = Math.Pow(height, 3) * width / 12;
            }
            else
            {
                momentoInercia = (Math.Pow(height, 3) * width - (Math.Pow(height - 2 * thickness, 3) * (width - 2 * thickness))) / 12;
            }

            return momentoInercia;
        }
    }
}
