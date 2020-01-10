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

        private readonly IBeamRequestValidator<T> _validator;
        private readonly IMappingResolver _mappingResolver;
        private readonly INewmarkMethod _newmarkMethod;

        public AbstractCalculateBeamVibration(
            IBeamRequestValidator<T> validator,
            IMappingResolver mappingResolver,
            INewmarkMethod newmarkMethod)
        {
            this._validator = validator;
            this._mappingResolver = mappingResolver;
            this._newmarkMethod = newmarkMethod;
        }

        protected override CalculateBeamResponse ProcessOperation(CalculateBeamRequest<T> request)
        {
            CalculateBeamResponse response = new CalculateBeamResponse();

            Beam beam = this.AddValues(request);

            BeamMatrix beamMatrix = this.CalculateParameters(beam);

            response.Data = this._newmarkMethod.Execute(beamMatrix.Mass, beamMatrix.Hardness, beamMatrix.Damping);

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