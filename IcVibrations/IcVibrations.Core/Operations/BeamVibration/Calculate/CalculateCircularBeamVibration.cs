using IcVibrations.Core.Calculator;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
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
    public class CalculateCircularBeamVibration : AbstractCalculateBeamVibration<CircularBeamRequestData>
    {
        private readonly IMappingResolver _mappingResolver;

        public CalculateCircularBeamVibration(
            IBeamRequestValidator<CircularBeamRequestData> validator,
            IMappingResolver mappingResolver,
            INewmarkMethod newmarkMethod) : base(validator, newmarkMethod, mappingResolver)
        {
            this._mappingResolver = mappingResolver;
        }

        protected override NewmarkMethodInput CalculateParameters(CalculateBeamRequest<CircularBeamRequestData> request, int degressFreedomMaximum, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}
