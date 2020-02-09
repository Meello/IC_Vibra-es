using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts.Beam.Calculate;

namespace IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber
{
    /// <summary>
    /// It represents the request content of CalculateBeamWithDva operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamWithDvaVibrationRequest<TProfile> : OperationRequestBase
        where TProfile : Profile
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="beamData"></param>
        /// <param name="methodParameterData"></param>
        public CalculateBeamWithDvaVibrationRequest(BeamWithDvaRequestData<TProfile> beamWithDvaData, NewmarkMethodParameter methodParameterData)
        {
            BeamWithDvaData = beamWithDvaData;
            NewmarkMethodParameter = methodParameterData;
        }

        public BeamWithDvaRequestData<TProfile> BeamWithDvaData { get; set; }

        public NewmarkMethodParameter NewmarkMethodParameter { get; set; }
    }
}
