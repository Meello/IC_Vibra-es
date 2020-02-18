using IcVibrations.Core.DTO.Input;

namespace IcVibrations.Core.NewmarkNumericalIntegration.Beam
{
    /// <summary>
    /// It's responsible to do the newmark numerical integration to the condition of a beam.
    /// </summary>
    public interface IBeamNewmarkMethod : INewmarkMethod<NewmarkMethodBeamInput>
    {
    }
}
