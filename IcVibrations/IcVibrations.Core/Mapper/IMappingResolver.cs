using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using IcVibrations.Models.Beam;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        CircularBeamWithDva BuildFrom(CircularBeamWithDvaRequestData requestData);

        CircularBeam BuildFrom(CircularBeamRequestData circularBeamRequestData);

        RectangularBeam BuildFrom(RectangularBeamRequestData rectangularBeamRequestData);

        RectangularPiezoelectric BuildFrom(PiezoelectricRequestData piezoelectricRequestData);

        OperationResponseData BuildFrom(NewmarkMethodOutput output);
    }
}
