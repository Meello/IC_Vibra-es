using IcVibrations.Common.Classes;
using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcVibrations.Core.Mapper
{
    public interface IMappingResolver
    {
        OperationResponseData BuildFrom(NewmarkMethodResponse output, string author, string analysisExplanation);

        Task<double[]> BuildFrom(List<Force> forces, uint degreesFreedomMaximum); 
    }
}