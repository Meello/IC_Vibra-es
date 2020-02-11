using IcVibrations.Common;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcVibrations.Core.Mapper
{
    public class MappingResolver : IMappingResolver
    {
        private readonly IArrayOperation _arrayOperation;

        public MappingResolver(
            IArrayOperation arrayOperation)
        {
            this._arrayOperation = arrayOperation;
        }

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

        public async Task<double[]> BuildFrom(List<Force> forces, uint degreesFreedomMaximum)
        {
            if(forces == null)
            {
                return null;
            }

            int i = 0;
            double[] forceValues = new double[forces.Count];
            uint[] forceNodePositions = new uint[forces.Count];

            foreach (Force force in forces)
            {
                forceValues[i] = force.Value;
                forceNodePositions[i] = force.NodePosition;
                i += 1;
            }

            return await this._arrayOperation.Create(forceValues, degreesFreedomMaximum, forceNodePositions, nameof(forceValues));
        }
    }
}
