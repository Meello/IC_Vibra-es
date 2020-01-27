using IcVibrations.Core.Models;
using IcVibrations.Core.Validators.Beam;
using IcVibrations.Core.Validators.MethodParameters;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Operations.PiezoelectricVibration.Calculate
{
    public abstract class CalculatePiezoelectricVibration : OperationBase<CalculatePiezoelectricRequest, CalculatePiezoelectricResponse>, ICalculatePiezoelectricVibration
    {
        private readonly IMethodParameterValidator _methodParameterValidator;
        private readonly IBeamRequestValidator<RectangularBeamRequestData> _beamRequestValidator;

        public CalculatePiezoelectricVibration(
            IMethodParameterValidator methodParameterValidator,
            IBeamRequestValidator<RectangularBeamRequestData> beamRequestValidator)
        {
            this._methodParameterValidator = methodParameterValidator;
            this._beamRequestValidator = beamRequestValidator;
        }

        protected override CalculatePiezoelectricResponse ProcessOperation(CalculatePiezoelectricRequest request)
        {
            CalculatePiezoelectricResponse response = new CalculatePiezoelectricResponse();

            uint degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);



            return response;
        }

        protected override CalculatePiezoelectricResponse ValidateOperation(CalculatePiezoelectricRequest request)
        {
            CalculatePiezoelectricResponse response = new CalculatePiezoelectricResponse();

            uint degreesFreedomMaximum = this.DegreesFreedomMaximum(request.BeamData.ElementCount);

            if (!this._methodParameterValidator.Execute(request.MethodParameterData, response))
            {
                return response;
            }

            // Validate piezoelectric

            if (!this._beamRequestValidator.Execute(request.BeamData, degreesFreedomMaximum, response))
            {
                return response;
            }

            return response;
        }

        public uint DegreesFreedomMaximum(uint element)
        {
            uint degreesFreedomMaximum = (element + 1) * Constants.DegreesFreedom;
            return degreesFreedomMaximum;
        }
    }
}
