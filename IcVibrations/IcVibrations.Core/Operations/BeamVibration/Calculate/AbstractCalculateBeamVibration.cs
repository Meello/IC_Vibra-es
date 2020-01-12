using IcVibrations.Core.Calculator;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.BeamRequest;
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
        protected abstract NewmarkMethodInput CalculateParameters(CalculateBeamRequest<T> request, int degressFreedomMaximum, OperationResponseBase response);

        //protected abstract string AnalysisExplanation();

        private readonly IBeamRequestValidator<T> _validator;
        private readonly INewmarkMethod _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;

        public AbstractCalculateBeamVibration(
            IBeamRequestValidator<T> validator,
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver)
        {
            this._validator = validator;
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest<T> request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            int degreesFredomMaximum = this.DegreesFreedomMaximum(request.Data.ElementCount);

            NewmarkMethodInput input = this.CalculateParameters(request, degreesFredomMaximum, response);

            NewmarkMethodOutput output = this._newmarkMethod.CreateOutput(input, response);

            response.Data = this._mappingResolver.BuildFrom(output);
            
            return response;
        }

        protected override CalculateBeamResponse ValidateOperation(CalculateBeamRequest<T> request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();
            
            if (!this._validator.Execute(request.Data, response))
            {
                return response;
            }
            
            return response;
        }
        
        private int DegreesFreedomMaximum(int nodes)
        {
            int degreesFreedomMaximum = nodes * Constants.DegreesFreedom;
            return degreesFreedomMaximum;
        }
    }
}