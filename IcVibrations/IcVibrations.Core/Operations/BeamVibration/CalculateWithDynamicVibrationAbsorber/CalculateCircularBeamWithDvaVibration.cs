using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.DataContracts.Beam.CalculateWithDynamicVibrationAbsorber;
using IcVibrations.Methods.NewmarkMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.CalculateWithDynamicVibrationAbsorber
{
    public class CalculateCircularBeamWithDvaVibration : AbstractCalculateBeamVibration<CircularBeamWithDvaRequestData>
    {
        public CalculateCircularBeamWithDvaVibration(
            IBeamRequestValidator<CircularBeamWithDvaRequestData> beamRequestValidator, 
            IMethodParameterValidator methodParameterValidator, 
            INewmarkMethod newmarkMethod, 
            IMappingResolver mappingResolver) : base(beamRequestValidator, methodParameterValidator, newmarkMethod, mappingResolver)
        {
        }

        protected override NewmarkMethodInput CalculateParameters(CalculateBeamRequest<CircularBeamWithDvaRequestData> request, uint degressFreedomMaximum, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}
