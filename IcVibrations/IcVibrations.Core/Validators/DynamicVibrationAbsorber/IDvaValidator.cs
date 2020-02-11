using IcVibrations.DataContracts;

namespace IcVibrations.Core.Validators.DynamicVibrationAbsorber
{
    public interface IDvaValidator
    {
        void Execute(BeamWithDvaRequestData requestData, OperationResponseBase response);
    }
}
