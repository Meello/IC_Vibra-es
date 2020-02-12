using IcVibrations.Common.Classes;
using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcVibrations.Core.Mapper
{
    public class MappingResolver : IMappingResolver
    {
        public OperationResponseData BuildFrom(NewmarkMethodResponse output, string author, string analysisExplanation)
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

        public Task<double[]> BuildFrom(List<Force> forces, uint degreesFreedomMaximum)
        {
            if(forces == null)
            {
                return null;
            }

            double[] force = new double[degreesFreedomMaximum];
            foreach (Force applyedForce in forces)
            {
                try
                {
                    force[applyedForce.NodePosition - 1] = applyedForce.Value;
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error creating force matrix. {ex.Message}.");
                }
            }

            return Task.FromResult(force);
        }
    }
}
