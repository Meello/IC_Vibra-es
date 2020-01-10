using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.Core.Validators.BeamRequest;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.BeamVibration.Calculate
{
    public class CalculateRectangularBeamVibration : AbstractCalculateBeamVibration<RectangularBeamRequestData>
    {
        private readonly IMappingResolver _mappingResolver;

        public CalculateRectangularBeamVibration(
            IBeamRequestValidator<RectangularBeamRequestData> validator, 
            IMappingResolver mappingResolver,
            INewmarkMethod newmarkMethod) : base(validator, newmarkMethod)
        {
            this._mappingResolver = mappingResolver;
        }

        protected override Beam AddValues(CalculateBeamRequest<RectangularBeamRequestData> request)
        {
            Beam beam = this._mappingResolver.AddValues(request.Data);

            return beam;
        }

        protected override string AnalysisExplanation(Beam beam)
        {
            // Ajustar a explicação
            string analysisExplanation = "The program analyse dymanic effects in a beam.";

            return analysisExplanation;
        }

        protected override BeamMatrix CalculateParameters(Beam beam)
        {
            return null;
        }
    }
}
