using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using System;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    /// <summary>
    /// It represents the request content of CalculateBeam operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamVibrationRequest<TProfile> : OperationRequestBase, ICalculateBeamVibrationRequest<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="beamData"></param>
        /// <param name="methodParameterData"></param>
        public CalculateBeamVibrationRequest(BeamRequestData<TProfile> beamData, NewmarkMethodParameter methodParameterData)
        {
            BeamData = beamData;
            MethodParameterData = methodParameterData;
        }

        public BeamRequestData<TProfile> BeamData { get; set; }

        public NewmarkMethodParameter MethodParameterData { get; set; }
    }
}
