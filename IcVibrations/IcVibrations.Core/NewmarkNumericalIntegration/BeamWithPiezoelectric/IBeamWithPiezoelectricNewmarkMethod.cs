using IcVibrations.Core.DTO.Input;

namespace IcVibrations.Core.NewmarkNumericalIntegration.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to do the newmark numerical integration to the condition of a beam with piezoelectric.
    /// </summary>
    public interface IBeamWithPiezoelectricNewmarkMethod : INewmarkMethod<NewmarkMethodBeamWithPiezoelectricInput>
    {
    }
}
