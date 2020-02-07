using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Core.Calculator;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public class CalculateCircularBeamVibration : AbstractCalculateBeamVibration<CircularBeamRequestData>
    {
        private readonly IArrayOperation _arrayOperation;
        private readonly IGeometricProperty _geometricProperty;
        private readonly IMappingResolver _mappingResolver;
        private readonly INewmarkMethod _newmarkMethod;

        public CalculateCircularBeamVibration(
            IArrayOperation arrayOperation,
            IGeometricProperty geometricProperty,
            IBeamRequestValidator<CircularBeamRequestData> beamRequestValidator,
            IMethodParameterValidator methodParameterValidator,
            IMappingResolver mappingResolver,
            INewmarkMethod newmarkMethod) : base(beamRequestValidator, methodParameterValidator, newmarkMethod, mappingResolver)
        {
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
            this._mappingResolver = mappingResolver;
            this._newmarkMethod = newmarkMethod;
        }

        protected override async Task<NewmarkMethodInput> CalculateParameters(CalculateBeamRequest<CircularBeamRequestData> request, uint degreesFreedomMaximum, OperationResponseBase response)
        {
            CircularBeam beam = this._mappingResolver.BuildFrom(request.BeamData);

            // Input values
            double area = this._geometricProperty.Area(request.BeamData.Diameter, request.BeamData.Thickness);

            double momentInertia = this._geometricProperty.MomentInertia(request.BeamData.Diameter, request.BeamData.Thickness);

            beam.GeometricProperty.Area = await this._arrayOperation.Create(area, request.BeamData.ElementCount);

            beam.GeometricProperty.MomentOfInertia = await this._arrayOperation.Create(momentInertia, request.BeamData.ElementCount);

            NewmarkMethodInput input = await this._newmarkMethod.CreateInput(request.MethodParameterData, beam, degreesFreedomMaximum);

            return input;
        }
    }
}
