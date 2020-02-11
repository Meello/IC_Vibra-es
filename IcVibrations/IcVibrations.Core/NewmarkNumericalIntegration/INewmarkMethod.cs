using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.Beam;
using IcVibrations.DataContracts;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    public interface INewmarkMethod<TBeam, TProfile>
        where TProfile : Profile, new()
        where TBeam : IBeam<TProfile>, new()
    {
        Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, TBeam beam);

        Task<NewmarkMethodOutput> CreateOutput(NewmarkMethodInput input, OperationResponseBase response);
    }
}
