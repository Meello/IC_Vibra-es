using IcVibrations.Core.Models;
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
    public abstract class AbstractBeamRequestValidator<T> : IBeamRequestValidator<T> where T : BeamRequestData
    {
        public bool Execute(T requestData, OperationResponseBase response)
        {
            // Validate nodes
            this.ValidateNodes(requestData.NodeCount, response);

            // Validate material
            this.ValidateMaterial(requestData.Material, response);

            // Validate profile
            this.ValidateShapeInput(requestData, response);

            return true;
        }

        protected abstract void ValidateShapeInput(T requestData, OperationResponseBase response);

        private void ValidateNodes(uint nodes, OperationResponseBase response)
        {
            if (nodes <= 2)
            {
                response.AddError("001", "Nodes should be greatter than or equal 2");
            }
        }

        private void ValidateMaterial(string material, OperationResponseBase response)
        {
            if (!Enum.TryParse(material.Trim(), true, out Materials materials))
            {
                response.AddError("002", $"Invalid material: {material}. Valid materials: {Enum.GetValues(typeof(Materials))}.");
            }
        }

        private void ValidateThickness(double thickness, OperationResponseBase response)
        {
            if (thickness <= 0)
            {
                response.AddError("007", $"Thickness: {thickness} must be greater than zero.");
            }
        }

        private void ValidateFastening(string fastening, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }

        private void ValidateLength(double length, OperationResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}
