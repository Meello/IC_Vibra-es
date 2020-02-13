using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts.CalculateVibration.Beam;

namespace IcVibrations.Core.Operations.Beam.Circular
{
    public interface ICalculateCircularBeamVibration : ICalculateVibration<CalculateBeamVibrationRequest<CircularProfile>, BeamRequestData<CircularProfile>, CircularProfile, Beam<CircularProfile>>
    {
    }
}
