﻿using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.CalculateVibration.BeamWithPiezoelectric
{
    /// <summary>
    /// It represents the request content of CalculatePiezoelectric operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamWithPiezoelectricVibrationRequest<TProfile> : CalculateVibrationRequest<TProfile, PiezoelectricRequestData<TProfile>>
        where TProfile : Profile, new()
    {
    }
}
