using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using System;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    /// <summary>
    /// It represents the request content of CalculateBeam operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamVibrationRequest<TProfile> : OperationRequestBase 
        where TProfile : Profile
    {
        public CalculateBeamVibrationRequest(BeamRequestData<TProfile> beamData, NewmarkMethodParameter methodParameterData)
        {
            BeamData = beamData;
            MethodParameterData = methodParameterData;
        }

        public BeamRequestData<TProfile> BeamData { get; set; }

        public NewmarkMethodParameter MethodParameterData { get; set; }
    }
}
