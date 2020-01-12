using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.AuxiliarMethods;
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
        private readonly INewmarkMethod _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;

        public CalculateRectangularBeamVibration(
            IArrayOperation arrayOperation,
            IGeometricProperty geometricProperty,
            IBeamRequestValidator<RectangularBeamRequestData> validator,
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver) : base(validator, newmarkMethod, mappingResolver)
        {
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override NewmarkMethodInput CalculateParameters(CalculateBeamRequest<RectangularBeamRequestData> request, int degreesFreedomMaximum, OperationResponseBase response)
        {
            Beam beam = this._mappingResolver.AddValues(request.Data);

            // Como ter acesso aos valores do perfil retangular?
            beam.Profile = new RectangularProfile
            {
                Height = request.Data.Height,
                Width = request.Data.Width,
                Thickness = request.Data.Thickness
            };

            // Input values
            double area = this._geometricProperty.Area(request.Data.Height, request.Data.Width, request.Data.Thickness);

            double momentInertia = this._geometricProperty.MomentInertia(request.Data.Height, request.Data.Width, request.Data.Thickness);

            beam.Profile.Area = this._arrayOperation.Create(area, request.Data.ElementCount);

            beam.Profile.MomentInertia = this._arrayOperation.Create(momentInertia, request.Data.ElementCount);

            NewmarkMethodInput input = this._newmarkMethod.CreateInput(request.Data, beam, degreesFreedomMaximum);

            return input;
        }
    }
}
