using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        OperationResponseData BuildFrom(NewmarkMethodOutput output, string author, string analysisExplanation);
    }
}