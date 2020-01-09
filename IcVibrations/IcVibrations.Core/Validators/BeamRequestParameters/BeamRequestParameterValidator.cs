using IcVibrations.Core.DTO;
using IcVibrations.Core.Validators.GreaterThanZero;
using IcVibrations.DataContracts;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequestParameters
{
    public class BeamRequestParameterValidator : IBeamRequestParameterValidator
    {
        private readonly IGreaterThanZeroValidator _greaterThanZeroValidator;

        public BeamRequestParameterValidator(
            IGreaterThanZeroValidator greaterThanZeroValidator)
        {
            this._greaterThanZeroValidator = greaterThanZeroValidator;
        }

        public void ValidateNodes(uint nodes, OperationResponseBase response)
        {
            if (nodes <= 2)
            {
                response.AddError("001", "Nodes should be greatter than or equal 2");
            }
        }

        public void ValidateMaterial(string material, OperationResponseBase response)
        {
            if (!Enum.TryParse(material.Trim(), true, out Materials materials))
            {
                response.AddError("002", $"Invalid material: {material}. Valid materials: {Enum.GetValues(typeof(Materials))}.");
            }
        }

        public void ValidateProfile(string profile, ProfileDimension profileDimension, OperationResponseBase response)
        {
            if (profile.Equals(Profiles.Rectangular.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                this._greaterThanZeroValidator.Execute(profileDimension.Height, nameof(profileDimension.Height), profile, response);

                this._greaterThanZeroValidator.Execute(profileDimension.Width, nameof(profileDimension.Width), profile, response);
            }
            else if (profile == Profiles.Circular.ToString())
            {
                this._greaterThanZeroValidator.Execute(profileDimension.Diameter, nameof(profileDimension.Diameter), profile, response);
            }
            else
            {
                response.AddError("006", $"Invalid profile: {profile}. Valid profiles: {Enum.GetValues(typeof(Profiles))}.");
            }
        }

        public void ValidateThickness(double thickness, OperationResponseBase response)
        {
            if(!this._greaterThanZeroValidator.Execute(thickness))
            {
                response.AddError("007",$"Thickness: {thickness} must be greater than zero.");
            }
        }

        public void ValidateFastening(string fastening, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }

        public void ValidateLength(double length, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}
