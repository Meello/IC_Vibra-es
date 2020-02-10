using IcVibrations.Common.Classes;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Methods.NewmarkMethod
{
    public interface INewmarkMethod<TBeam>
        where TBeam : AbstractBeam, new()
    {
        Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, TBeam beam);

        Task<NewmarkMethodOutput> CreateOutput(NewmarkMethodInput input, OperationResponseBase response);
    }
}
