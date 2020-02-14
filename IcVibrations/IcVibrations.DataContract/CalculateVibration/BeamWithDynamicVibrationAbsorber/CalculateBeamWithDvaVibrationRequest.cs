using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber
{
    /// <summary>
    /// It represents the request content of CalculateBeamWithDva operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamWithDvaVibrationRequest<TProfile> : CalculateVibrationRequest<TProfile, BeamWithDvaRequestData<TProfile>>
        where TProfile : Profile
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="beamData"></param>
        /// <param name="methodParameterData"></param>
        public CalculateBeamWithDvaVibrationRequest(BeamWithDvaRequestData<TProfile> beamWithDvaData, NewmarkMethodParameter methodParameterData) : base(beamWithDvaData, methodParameterData)
        {
        }
    }
}
