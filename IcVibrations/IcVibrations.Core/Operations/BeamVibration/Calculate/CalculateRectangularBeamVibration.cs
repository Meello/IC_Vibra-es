using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public class CalculateRectangularBeamVibration : AbstractCalculateBeamVibration<RectangularBeamRequestData>
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly IGeometricProperty _geometricProperty;
        private readonly IMappingResolver _mappingResolver;

        public CalculateRectangularBeamVibration(
            IArrayOperation arrayOperation,
            IGeometricProperty geometricProperty,
            IBeamRequestValidator<RectangularBeamRequestData> validator, 
            IMappingResolver mappingResolver,
            INewmarkMethod newmarkMethod) : base(validator, newmarkMethod, mappingResolver)
        {
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
            this._mappingResolver = mappingResolver;
        }

        protected override void CalculateParameters(CalculateBeamRequest<RectangularBeamRequestData> request, Beam beam, int elementCount, int degressFreedomMaximum)
        {
            beam.Profile = new RectangularProfile
            {
                Height = request.Data.Height,
                Width = request.Data.Width,
                Thickness = request.Data.Thickness
            };

            double area = this._geometricProperty.Area(request.Data.Height, request.Data.Width, request.Data.Thickness);

            double momentInertia = this._geometricProperty.MomentInertia(request.Data.Height, request.Data.Width, request.Data.Thickness);

            double[,] mass;

            double[,] hardness;

            double[,] damping;

            beam.Profile.Area = this._arrayOperation.Create(area, elementCount);

        }                                              
    }
}
