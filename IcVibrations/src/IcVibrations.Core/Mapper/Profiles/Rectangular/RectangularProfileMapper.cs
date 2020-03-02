﻿using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Mapper.Profiles.Rectangular
{
    public class RectangularProfileMapper : ProfileMapper<RectangularProfile>, IRectangularProfileMapper
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _calculateGeometricProperty;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="calculateGeometricProperty"></param>
        public RectangularProfileMapper(
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty calculateGeometricProperty)
        {
            this._calculateGeometricProperty = calculateGeometricProperty;
            this._arrayOperation = arrayOperation;
        }

        public async override Task<GeometricProperty> Execute(RectangularProfile profile, uint degreesFreedomMaximum)
        {
            GeometricProperty geometricProperty = new GeometricProperty();

            double area = await this._calculateGeometricProperty.Area(profile.Height, profile.Width, profile.Thickness);
            double momentOfInertia = await this._calculateGeometricProperty.MomentOfInertia(profile.Height, profile.Width, profile.Thickness);

            geometricProperty.Area = await this._arrayOperation.Create(area, degreesFreedomMaximum);
            geometricProperty.MomentOfInertia = await this._arrayOperation.Create(momentOfInertia, degreesFreedomMaximum);

            return geometricProperty;
        }
    }
}
