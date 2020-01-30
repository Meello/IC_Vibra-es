using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;

namespace IcVibrations.Core.Validators.DynamicVibrationAbsorber
{
    public interface IDvaValidator
    {
        void Execute(BeamWithDvaRequestData requestData, OperationResponseBase response);
    }
}
