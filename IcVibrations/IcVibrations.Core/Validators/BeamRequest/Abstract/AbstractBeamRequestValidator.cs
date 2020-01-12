using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;
using static IcVibrations.Common.Enum;

namespace IcVibrations.Core.Validators.BeamRequest
{
    public abstract class AbstractBeamRequestValidator<T> : IBeamRequestValidator<T> where T : BeamRequestData
    {
        public bool Execute(T requestData, OperationResponseBase response)
        {
            this.ValidateElementCount(requestData.ElementCount, response);

            this.ValidateMaterial(requestData.Material, response);

            this.ValidateThickness(requestData.Thickness, response);

            this.ValidateFastening(requestData.FirstFastening, response);

            this.ValidateFastening(requestData.LastFastening, response);

            this.ValidateLength(requestData.Length, response);

            this.ValidateShapeInput(requestData, response);

            return true;
        }

        protected abstract void ValidateShapeInput(T requestData, OperationResponseBase response);

        private void ValidateElementCount(int elementCount, OperationResponseBase response)
        {
            if (elementCount <= 1)
            {
                response.AddError("001", "Elements should be greatter than zero.");
            }
        }

        private void ValidateMaterial(string material, OperationResponseBase response)
        {
            //_ => descarte, não importa a variável
            if (!Enum.TryParse(material.Trim(), ignoreCase: true, out Materials _))
            {
                response.AddError("002", $"Invalid material: {material}. Valid materials: {Enum.GetValues(typeof(Materials))}.");
            }
        }

        private void ValidateThickness(double thickness, OperationResponseBase response)
        {
            if (thickness <= 0)
            {
                response.AddError("003", $"Thickness: {thickness} must be greater than zero.");
            }
        }

        private void ValidateFastening(string fastening, OperationResponseBase response)
        {
            if (!Enum.TryParse(fastening, true, out Fastenings fastenings))
            {
                response.AddError("004", $"Invalid fastening: {fastening}. Valid fastenings: {Enum.GetValues(typeof(Fastenings))}.");
            }
        }

        private void ValidateLength(double length, OperationResponseBase response)
        {
            if (length <= 0)
            {
                response.AddError("005", $"Length: {length} must be greater than zero.");
            }
        }
    }
}
