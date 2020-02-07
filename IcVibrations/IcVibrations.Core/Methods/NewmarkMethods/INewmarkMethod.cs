using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.Models.Beam;
using System.Threading.Tasks;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod
    {
        Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, BeamWithDva beam, uint degreesFreedomMaximum);

        Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, Beam beam, uint degreesFreedomMaximum);

        Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, RectangularBeam beam, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum);

        Task<NewmarkMethodOutput> CreateOutput(NewmarkMethodInput input, OperationResponseBase response);
    }
}
