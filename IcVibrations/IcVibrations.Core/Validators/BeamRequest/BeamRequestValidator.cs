using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models;
using IcVibrations.Core.Validators.BeamRequestParameters;
using IcVibrations.Core.Validators.GreaterThanZero;
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
        private readonly IGreaterThanZeroValidator _biggerThanZeroValidator;
        private readonly IBeamRequestParameterValidator _parameterValidator;
        private readonly IMappingResolver _mappingresolver;

        public BeamRequestValidator(
            IGreaterThanZeroValidator biggerThanZeroValidator,
            IBeamRequestParameterValidator parameterValidator,
            IMappingResolver mappingresolver)
        {
            this._biggerThanZeroValidator = biggerThanZeroValidator;
            this._parameterValidator = parameterValidator;
            this._mappingresolver = mappingresolver;
        }

        public bool Execute(BeamRequestData requestData, OperationResponseBase response)
        {
            // Validate nodes
            this._parameterValidator.ValidateNodes(requestData.NodeCount, response);

            // Validate material
            this._parameterValidator.ValidateMaterial(requestData.Material, response);

            // Validate profile
            this._parameterValidator.ValidateProfile(requestData.Profile, this._mappingresolver.BuildFrom(requestData), response);



            return true;
        }
    }
}
