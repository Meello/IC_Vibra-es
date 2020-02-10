using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.Profiles
{
    public class RectangularProfileBuilder : ProfileBuilder<RectangularProfile>
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _calculateGeometricProperty;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="calculateGeometricProperty"></param>
        public RectangularProfileBuilder(
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty calculateGeometricProperty)
        {
            this._arrayOperation = arrayOperation;
            this._calculateGeometricProperty = calculateGeometricProperty;
        }

        public async override Task<GeometricProperty> Execute(RectangularProfile profile, uint degreesFreedomMaximum)
        {
            GeometricProperty geometricProperty = new GeometricProperty();

            double area = await this._calculateGeometricProperty.Area(profile.Height, profile.Width, profile.Thickness.Value);
            double momentOfInertia = await this._calculateGeometricProperty.MomentOfInertia(profile.Height, profile.Width, profile.Thickness.Value);

            geometricProperty.Area = await this._arrayOperation.Create(area, degreesFreedomMaximum);
            geometricProperty.MomentOfInertia = await this._arrayOperation.Create(momentOfInertia, degreesFreedomMaximum);

            return geometricProperty;
        }
    }
}
