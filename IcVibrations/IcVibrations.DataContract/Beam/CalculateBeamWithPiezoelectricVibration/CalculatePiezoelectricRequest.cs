using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.Beam.CalculateBeamWithPiezoelectricVibration
{
    /// <summary>
    /// It represents the request content of CalculatePiezoelectric operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculatePiezoelectricRequest<TProfile> : OperationRequestBase
        where TProfile : Profile
    {
        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="beamWithPiezoelectricData"></param>
        /// <param name="methodParameterData"></param>
        public CalculatePiezoelectricRequest(PiezoelectricRequestData<TProfile> beamWithPiezoelectricData, NewmarkMethodParameter methodParameterData)
        {
            BeamWithPiezoelectricData = beamWithPiezoelectricData;
            MethodParameterData = methodParameterData;
        }

        public PiezoelectricRequestData<TProfile> BeamWithPiezoelectricData { get; }

        public NewmarkMethodParameter MethodParameterData { get; }
    }
}
