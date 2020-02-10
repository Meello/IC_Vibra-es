using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;

namespace IcVibrations.Core.Mapper
{
    public class MappingResolver : IMappingResolver
    {
        public OperationResponseData BuildFrom(NewmarkMethodOutput output, string author, string analysisExplanation)
        {
            if(output == null || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(analysisExplanation))
            {
                return null;
            }

            return new OperationResponseData()
            {
                Author = author,
                AnalysisExplanation = analysisExplanation,
                AnalysisResults = output.Analyses
            };
        }
    }
}
