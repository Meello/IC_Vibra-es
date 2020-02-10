using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using System;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    /// <summary>
    /// It represents the request content of CalculateBeam operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamVibrationRequest<TProfile, TRequestData> : OperationRequestBase, ICalculateBeamVibrationRequest<TProfile, TRequestData>
        where TProfile : Profile
        where TRequestData : IBeamRequestData<TProfile>
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="beamData"></param>
        /// <param name="methodParameterData"></param>
        public CalculateBeamVibrationRequest(TRequestData beamData, NewmarkMethodParameter methodParameterData)
        {
            BeamData = beamData;
            MethodParameterData = methodParameterData;
        }

        public TRequestData BeamData { get; set; }

        public NewmarkMethodParameter MethodParameterData { get; set; }
    }
}
