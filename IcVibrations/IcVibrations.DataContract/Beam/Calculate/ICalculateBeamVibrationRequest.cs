using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.Beam.Calculate
{
    /// <summary>
    /// It represents the request content of CalculateBeam operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface ICalculateBeamVibrationRequest<TProfile>
        where TProfile : Profile
    {
        BeamRequestData<TProfile> BeamData { get; set; }

        NewmarkMethodParameter MethodParameterData { get; set; }
    }
}