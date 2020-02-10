using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.Profiles
{
    /// <summary>
    /// It's responsible to calculate the vibration in a circular beam.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TBeam"></typeparam>
    public abstract class CalculateRectangulaProfileVibration<TRequest, TBeam> : CalculateVibration<TRequest, RectangularProfile, TBeam>
        where TRequest : OperationRequestBase, ICalculateBeamVibrationRequest<RectangularProfile>
        where TBeam : IBeam, new()
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _calculateGeometricProperty;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="profileMapper"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateRectangulaProfileVibration(
            INewmarkMethod<TBeam> newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<RectangularProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation,
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty calculateGeometricProperty)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
            this._calculateGeometricProperty = calculateGeometricProperty;
        }

        public async override Task<GeometricProperty> CalculateGeometricProperties(RectangularProfile profile, uint degreesFreedomMaximum)
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
