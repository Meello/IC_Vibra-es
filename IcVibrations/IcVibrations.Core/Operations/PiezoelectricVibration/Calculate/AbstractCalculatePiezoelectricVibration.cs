using IcVibrations.Core.Models;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.Piezoelectric.Calculate
{
    public abstract class AbstractCalculatePiezoelectricVibration<TPiezoelectric, TBeam> : OperationBase<CalculatePiezoelectricRequest<TPiezoelectric, TBeam>, CalculatePiezoelectricResponse> 
        where TPiezoelectric: PiezoelectricRequestData
        where TBeam: BeamRequestData
    {
        private readonly IMethodParameterValidator _methodParameterValidator;
        private readonly IBeamRequestValidator<TBeam> _beamRequestValidator;

        public AbstractCalculatePiezoelectricVibration(
            IMethodParameterValidator methodParameterValidator,
            IBeamRequestValidator<TBeam> beamRequestValidator)
        {
            this._methodParameterValidator = methodParameterValidator;
            this._beamRequestValidator = beamRequestValidator;
        }

        protected override CalculatePiezoelectricResponse ProcessOperation(CalculatePiezoelectricRequest<TPiezoelectric, TBeam> request)
        {
            CalculatePiezoelectricResponse response = new CalculatePiezoelectricResponse();

            int degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            return response;
        }

        protected override CalculatePiezoelectricResponse ValidateOperation(CalculatePiezoelectricRequest<TPiezoelectric, TBeam> request)
        {
            CalculatePiezoelectricResponse response = new CalculatePiezoelectricResponse();

            int degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            if (!this._methodParameterValidator.Execute(request.MethodParameterData, response))
            {
                return response;
            }

            // Validar piezoelectric

            if (!this._beamRequestValidator.Execute(request.BeamData, degreesFreedomMaximum, response))
            {
                return response;
            }

            return response;
        }

        public int DegreesFreedomMaximum(int element)
        {
            int degreesFreedomMaximum = (element + 1) * Constants.DegreesFreedom;
            return degreesFreedomMaximum;
        }
    }
}
