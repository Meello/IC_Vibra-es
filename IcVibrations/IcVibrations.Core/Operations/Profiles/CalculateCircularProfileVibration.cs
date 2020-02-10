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
    /// It's responsible to calculate the vibration in a rectangular beam.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TBeam"></typeparam>
    public abstract class CalculateCircularProfileVibration<TRequest, TBeam> : CalculateVibration<TRequest, CircularProfile, TBeam>
        where TRequest : OperationRequestBase, ICalculateBeamVibrationRequest<CircularProfile>
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
        public CalculateCircularProfileVibration(
            INewmarkMethod<TBeam> newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<CircularProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation,
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty calculateGeometricProperty)
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
            this._calculateGeometricProperty = calculateGeometricProperty;
        }

        public async override Task<GeometricProperty> CalculateGeometricProperties(CircularProfile profile, uint degreesFreedomMaximum)
        {
            GeometricProperty geometricProperty = new GeometricProperty();

            double area = await this._calculateGeometricProperty.Area(profile.Diameter, profile.Thickness.Value);
            double momentOfInertia = await this._calculateGeometricProperty.MomentOfInertia(profile.Diameter, profile.Thickness.Value);

            geometricProperty.Area = await this. _arrayOperation.Create(area, degreesFreedomMaximum);
            geometricProperty.MomentOfInertia = await this._arrayOperation.Create(momentOfInertia, degreesFreedomMaximum);

            return geometricProperty;
        }
    }
}
