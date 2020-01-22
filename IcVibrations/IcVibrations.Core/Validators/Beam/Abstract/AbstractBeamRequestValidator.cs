using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static IcVibrations.Common.Enum;

namespace IcVibrations.Core.Validators.Beam
{
    public abstract class AbstractBeamRequestValidator<T> : IBeamRequestValidator<T> 
        where T : BeamRequestData
    {
        public bool Execute(T beamData, int degreesFreedomMaximum, OperationResponseBase response)
        {
            this.ValidateElementCount(beamData.ElementCount, response);

            this.ValidateMaterial(beamData.Material, response);

            this.ValidateThickness(beamData.Thickness, response);

            this.ValidateFastening(beamData.FirstFastening, response);

            this.ValidateFastening(beamData.LastFastening, response);

            this.ValidateLength(beamData.Length, response);

            this.ValidateForceValueAndPosition(beamData.Forces, beamData.ForceNodePositions, degreesFreedomMaximum, response);
            
            this.ValidateProfileInput(beamData, response);

            if(response.Errors.Count > 0)
            {
                return false;
            }

            return true;
        }

        protected abstract void ValidateProfileInput(T beamData, OperationResponseBase response);

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
            if (!Enum.TryParse(material, ignoreCase: false, out Materials _))
            {
                response.AddError("002", $"Invalid material: {material}. Valid materials: {Enum.GetValues(typeof(Materials)).ToString()}.");
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

        private void ValidateForce(double[] forces, int degreesFreedomMaximum, OperationResponseBase response)
        {
            if(forces.Count() <= 0 || forces.Count() > degreesFreedomMaximum)
            {
                response.AddError("009",$"Invalid number of forces: {forces.Count()}. Min: 0. Max: {degreesFreedomMaximum}.");
            }
            else if(forces.Contains(0))
            {
                response.AddError("010","Forces can't contain value zero.");
            }
        }

        private void ValidateForceNodePosition(int[] forcePositions, int degreesFreedomMaximum, OperationResponseBase response)
        {
            if (forcePositions.Count() <= 0 || forcePositions.Count() > degreesFreedomMaximum)
            {
                response.AddError("011", $"Invalid number of positionss: {forcePositions.Count()}. Min: 0. Max: {degreesFreedomMaximum}.");
            }
            else if (forcePositions.Any(p => p >= degreesFreedomMaximum))
            {
                response.AddError("012", $"Positions can't contain value greater than or equal {degreesFreedomMaximum}.");
            }
        }

        private void ValidateForceValueAndPosition(double[] forces, int[] forceNodePositions, int degreesFreedomMaximum, OperationResponseBase response)
        {
            this.ValidateForce(forces, degreesFreedomMaximum, response);

            this.ValidateForceNodePosition(forceNodePositions, degreesFreedomMaximum, response);

            if(forces.Count() != forceNodePositions.Count())
            {
                response.AddError("013",$"Number of forces: {forces.Count()} and number of force node positions: {forceNodePositions.Count()} must be equal.");
            }
        }
    }
}
