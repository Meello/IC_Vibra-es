using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Validators.BeamRequestParameters
{
    public interface IBeamRequestParameterValidator
    {
        void ValidateNodes(uint nodes, OperationResponseBase response);

        void ValidateMaterial(string material, OperationResponseBase response);

        void ValidateProfile(string profile, ProfileDimension profileDimension, OperationResponseBase response);

        void ValidateThickness(double thickness, OperationResponseBase response);

        void ValidateFastening(string fastening, OperationResponseBase response);

        void ValidateLength(double length, OperationResponseBase response);
    }
}
