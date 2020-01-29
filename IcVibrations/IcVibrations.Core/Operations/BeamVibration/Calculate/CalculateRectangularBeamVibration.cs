using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.AuxiliarOperations;
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
            IBeamRequestValidator<RectangularBeamRequestData> beamRequestValidator,
            IMethodParameterValidator methodParameterValidator,
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver) : base(beamRequestValidator, methodParameterValidator, newmarkMethod, mappingResolver)
        {
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override NewmarkMethodInput CalculateParameters(CalculateBeamRequest<RectangularBeamRequestData> request, uint degreesFreedomMaximum, OperationResponseBase response)
        {
            RectangularBeam beam = this._mappingResolver.BuildFrom(request.BeamData);

            // Input values
            double area = this._geometricProperty.Area(request.BeamData.Height, request.BeamData.Width, request.BeamData.Thickness);

            double momentInertia = this._geometricProperty.MomentInertia(request.BeamData.Height, request.BeamData.Width, request.BeamData.Thickness);

            beam.GeometricProperty.Area = this._arrayOperation.Create(area, request.BeamData.ElementCount);

            beam.GeometricProperty.MomentInertia = this._arrayOperation.Create(momentInertia, request.BeamData.ElementCount);

            NewmarkMethodInput input = this._newmarkMethod.CreateInput(request.MethodParameterData, beam, degreesFreedomMaximum);

            return input;
        }
    }
}
