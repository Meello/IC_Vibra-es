using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Mapper.Profiles
{
    /// <summary>
    /// It's responsible to build a circular profile.
    /// </summary>
    public class CircularProfileMapper : ProfileMapper<CircularProfile>
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _calculateGeometricProperty;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="calculateGeometricProperty"></param>
        public CircularProfileMapper(
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty calculateGeometricProperty)
        {
            _arrayOperation = arrayOperation;
            _calculateGeometricProperty = calculateGeometricProperty;
        }

        public async override Task<GeometricProperty> Execute(CircularProfile profile, uint degreesFreedomMaximum)
        {
            GeometricProperty geometricProperty = new GeometricProperty();

            double area = await _calculateGeometricProperty.Area(profile.Diameter, profile.Thickness.Value);
            double momentOfInertia = await _calculateGeometricProperty.MomentOfInertia(profile.Diameter, profile.Thickness.Value);

            geometricProperty.Area = await _arrayOperation.Create(area, degreesFreedomMaximum);
            geometricProperty.MomentOfInertia = await _arrayOperation.Create(momentOfInertia, degreesFreedomMaximum);

            return geometricProperty;
        }
    }
}
