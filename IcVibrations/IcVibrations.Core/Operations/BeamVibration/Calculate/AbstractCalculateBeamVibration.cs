using IcVibrations.Core.Calculator;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public abstract class AbstractCalculateBeamVibration<T> : OperationBase<CalculateBeamRequest<T>, CalculateBeamResponse> 
        where T : BeamRequestData
    {
        protected abstract NewmarkMethodInput CalculateParameters(CalculateBeamRequest<T> request, uint degressFreedomMaximum, OperationResponseBase response);

        //protected abstract string AnalysisExplanation();

        private readonly IBeamRequestValidator<T> _beamRequestValidator;
        private readonly IMethodParameterValidator _methodParameterValidator;
        private readonly INewmarkMethod _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;

        public AbstractCalculateBeamVibration(
            IBeamRequestValidator<T> beamRequestValidator,
            IMethodParameterValidator methodParameterValidator,
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver)
        {
            this._beamRequestValidator = beamRequestValidator;
            this._methodParameterValidator = methodParameterValidator;
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest<T> request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            uint degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            NewmarkMethodInput input = this.CalculateParameters(request, degreesFreedomMaximum, response);

            NewmarkMethodOutput output = this._newmarkMethod.CreateOutput(input, response);

            response.Data = this._mappingResolver.BuildFrom(output);
            
            return response;
        }

        protected override CalculateBeamResponse ValidateOperation(CalculateBeamRequest<T> request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            uint degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            if(!this._methodParameterValidator.Execute(request.MethodParameterData, response))
            {
                return response;
            }

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