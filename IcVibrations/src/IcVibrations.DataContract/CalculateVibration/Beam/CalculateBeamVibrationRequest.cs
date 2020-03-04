﻿using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.CalculateVibration.Beam
{
    /// <summary>
    /// It represents the request content of CalculateBeam operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class CalculateBeamVibrationRequest<TProfile> : CalculateVibrationRequest<TProfile, BeamRequestData<TProfile>>
        where TProfile : Profile, new()
    {
    }
}
