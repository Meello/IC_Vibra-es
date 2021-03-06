﻿using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;

namespace IcVibrations.DataContracts.CalculateVibration
{
    /// <summary>
    /// It represents the request content of CalculateBeam operations.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateVibrationRequest<TProfile, TRequestData> : OperationRequestBase
        where TProfile : Profile
        where TRequestData : CalculateVibrationRequestData<TProfile>
    {
        public TRequestData BeamData { get; set; }

        public NewmarkMethodParameter MethodParameterData { get; set; }
    }
}
