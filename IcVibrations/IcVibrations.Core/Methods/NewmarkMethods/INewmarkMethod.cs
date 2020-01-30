using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.Models.Beam;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod
    {
        NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, BeamWithDva beam, uint degreesFreedomMaximum);

        NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, Beam beam, uint degreesFreedomMaximum);

        NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, RectangularBeam beam, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum);

        NewmarkMethodOutput CreateOutput(NewmarkMethodInput input, OperationResponseBase response);
    }
}
