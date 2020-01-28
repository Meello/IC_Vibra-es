using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.PiezoelectricVibration.Calculate
{
    public class CalculatePiezoelectricVibration : OperationBase<CalculatePiezoelectricRequest, CalculatePiezoelectricResponse>, ICalculatePiezoelectricVibration
    {
        private readonly IMethodParameterValidator _methodParameterValidator;
        private readonly IBeamRequestValidator<RectangularBeamRequestData> _beamRequestValidator;
        private readonly INewmarkMethod _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;

        public CalculatePiezoelectricVibration(
            IMethodParameterValidator methodParameterValidator,
            IBeamRequestValidator<RectangularBeamRequestData> beamRequestValidator,
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver)
        {
            this._methodParameterValidator = methodParameterValidator;
            this._beamRequestValidator = beamRequestValidator;
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override CalculatePiezoelectricResponse ProcessOperation(CalculatePiezoelectricRequest request)
        {
            CalculatePiezoelectricResponse response = new CalculatePiezoelectricResponse();

            Beam beam = this._mappingResolver.BuildFrom(request.BeamData);

            Piezoelectric piezoelectric = this._mappingResolver.BuildFrom(request.PiezoelectricRequestData);

            uint degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            NewmarkMethodInput input = this._newmarkMethod.CreateInput(request.MethodParameterData,beam,piezoelectric,degreesFreedomMaximum);

            NewmarkMethodOutput output = this._newmarkMethod.CreateOutput(input, response);

            return response;
        }

        protected override CalculatePiezoelectricResponse ValidateOperation(CalculatePiezoelectricRequest request)
        {
            CalculatePiezoelectricResponse response = new CalculatePiezoelectricResponse();

            uint degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            if (!this._methodParameterValidator.Execute(request.MethodParameterData, response))
            {
                return response;
            }

            // Validate piezoelectric

            if (!this._beamRequestValidator.Execute(request.BeamData, degreesFreedomMaximum, response))
            {
                return response;
            }

            return response;
        }

        public uint DegreesFreedomMaximum(uint element)
        {
            uint degreesFreedomMaximum = (element + 1) * Constants.DegreesFreedom;
            return degreesFreedomMaximum;
        }
    }
}
