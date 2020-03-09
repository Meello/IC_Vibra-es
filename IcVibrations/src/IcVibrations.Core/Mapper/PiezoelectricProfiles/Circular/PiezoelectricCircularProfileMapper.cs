using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Mapper.PiezoelectricProfiles.Circular
{
    /// <summary>
    /// It's responsible to build the piezoelectric circular profile.
    /// </summary>
    public class PiezoelectricCircularProfileMapper : PiezoelectricProfileMapper<CircularProfile>, IPiezoelectricCircularProfileMapper
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _calculateGeometricProperty;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="calculateGeometricProperty"></param>
        public PiezoelectricCircularProfileMapper(
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty calculateGeometricProperty)
        {
            this._arrayOperation = arrayOperation;
            this._calculateGeometricProperty = calculateGeometricProperty;
        }

        public async override Task<GeometricProperty> Execute(CircularProfile profile, uint numberOfPiezoelectricsPerElements, uint[] elementsWithPiezoelectric, uint degreesFreedomMaximum)
        {
            GeometricProperty geometricProperty = new GeometricProperty();

            double uniqueArea = await this._calculateGeometricProperty.Area(profile.Diameter, profile.Thickness.Value);
            double uniqueMomentOfInertia = await this._calculateGeometricProperty.MomentOfInertia(profile.Diameter, profile.Thickness.Value);

            double area = uniqueArea * numberOfPiezoelectricsPerElements;
            double momentOfInertia = uniqueMomentOfInertia * numberOfPiezoelectricsPerElements;

            geometricProperty.Area = await this._arrayOperation.Create(area, degreesFreedomMaximum, elementsWithPiezoelectric, nameof(area));
            geometricProperty.MomentOfInertia = await this._arrayOperation.Create(momentOfInertia, degreesFreedomMaximum, elementsWithPiezoelectric, nameof(momentOfInertia));

            return geometricProperty;
        }
    }
}
