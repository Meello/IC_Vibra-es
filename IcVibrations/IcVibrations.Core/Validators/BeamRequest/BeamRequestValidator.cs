using IcVibrations.Core.Models;
using IcVibrations.Core.Validators.BeamRequestParameters;
using IcVibrations.Core.Validators.BiggerThanZero;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequest
{
    public class BeamRequestValidator : IBeamRequestValidator
    {
        private readonly IBiggerThanZeroValidator _biggerThanZeroValidator;
        private readonly IBeamRequestParameterValidator _parameterValidator;

        public BeamRequestValidator(
            IBiggerThanZeroValidator biggerThanZeroValidator,
            IBeamRequestParameterValidator parameterValidator)
        {
            this._biggerThanZeroValidator = biggerThanZeroValidator;
            this._parameterValidator = parameterValidator;
        }

        public bool Execute(BeamRequestData requestData, OperationResponseBase response)
        {
            this._parameterValidator.ValidateNodes(requestData.Nodes, response);

            if (!Enum.TryParse(requestData.Material.Trim(), true, out Materials materials))
            {
                response.AddError("002",$"Invalid material: {requestData.Material}. Valid materials: {Enum.GetValues(typeof(Materials))}.");
            }
            
            if(requestData.Profile == "Rectangle")
            {
                if(!this._biggerThanZeroValidator.Execute(requestData.Height))
                {
                    response.AddError("003",$"Height: {requestData.Height} must be bigger than zero to profile: {requestData.Profile}.");
                }

                if (!this._biggerThanZeroValidator.Execute(requestData.Width))
                {
                    response.AddError("004", $"Width: {requestData.Width} must be bigger than zero to profile: {requestData.Profile}.");
                }

                if (requestData.Diameter != null)
                {
                    response.AddError("005", $"Diameter: {requestData.Diameter} must be null to profile: {requestData.Profile}.");
                }
            }

            if(!this._biggerThanZeroValidator.Execute(requestData.Diameter))
            {

            }

            return true;
        }
    }
}
