using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts.CalculateVibration.Beam;

namespace IcVibrations.Core.Operations.Beam.Rectangular
{
    public interface ICalculateRectangularBeamVibration : ICalculateVibration<CalculateBeamVibrationRequest<RectangularProfile>, BeamRequestData<RectangularProfile>, RectangularProfile, Beam<RectangularProfile>>
    {
    }
}
