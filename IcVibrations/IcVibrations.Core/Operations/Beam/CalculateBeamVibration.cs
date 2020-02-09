using IcVibrations.Common.Profiles;
using IcVibrations.DataContracts.Beam.Calculate;
using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.Beam
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamVibration<TProfile> : OperationBase<CalculateBeamVibrationRequest<TProfile>, CalculateBeamVibrationResponse>, ICalculateBeamVibration<TProfile>
        where TProfile : Profile
    {
        public abstract bool ValidateProfile(TProfile profile);

        public abstract void BuildProfile(TProfile profile);

        protected override Task<CalculateBeamVibrationResponse> ProcessOperation(CalculateBeamVibrationRequest<TProfile> request)
        {
            throw new NotImplementedException();
        }

        protected override Task<CalculateBeamVibrationResponse> ValidateOperation(CalculateBeamVibrationRequest<TProfile> request)
        {
            throw new NotImplementedException();
        }
    }
}
