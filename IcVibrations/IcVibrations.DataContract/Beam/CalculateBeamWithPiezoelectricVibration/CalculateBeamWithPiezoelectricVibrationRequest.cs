using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts.Beam.Calculate;

namespace IcVibrations.DataContracts.Beam.CalculateBeamWithPiezoelectricVibration
{
    /// <summary>
    /// It represents the request content of CalculatePiezoelectric operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamWithPiezoelectricRequest<TProfile> : CalculateBeamVibrationRequest<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="beamWithPiezoelectricData"></param>
        /// <param name="methodParameterData"></param>
        public CalculateBeamWithPiezoelectricRequest(PiezoelectricRequestData<TProfile> beamWithPiezoelectricData, NewmarkMethodParameter methodParameterData) : base(beamWithPiezoelectricData, methodParameterData)
        {
        }
    }
}
