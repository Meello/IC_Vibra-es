using IcVibrations.Core.Calculator;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Validators.BeamRequest;
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
        protected abstract Beam AddValues(CalculateBeamRequest<T> request);

        protected abstract BeamMatrix CalculateParameters(Beam beam);

        protected abstract string AnalysisExplanation();

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

            Beam beam = this.AddValues(request);

            BeamMatrix beamMatrix = this.CalculateParameters(beam);

            this._mappingResolver.AddValues(values: beamMatrix, local: beam);

            string analysisExplanation = this.AnalysisExplanation();

            NewmarkMethodOutput newmarkMethodOutput = this._newmarkMethod.Execute(beam.Mass, beam.Hardness, beam.Damping);

            response.Data = this._mappingResolver.BuildFrom(newmarkMethodOutput, analysisExplanation);
            
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
    }
}