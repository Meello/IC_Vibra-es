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

namespace IcVibrations.Core.Validators.BeamRequest
{
    public abstract class AbstractBeamRequestValidator<T> : IBeamRequestValidator<T> where T : BeamRequestData
    {
        public bool Execute(T requestData, int degreesFreedomMaximum, OperationResponseBase response)
        {
            this.ValidateElementCount(requestData.ElementCount, response);

            this.ValidateMaterial(requestData.Material, response);

            this.ValidateThickness(requestData.Thickness, response);

            this.ValidateFastening(requestData.FirstFastening, response);

            this.ValidateFastening(requestData.LastFastening, response);

            this.ValidateLength(requestData.Length, response);

            this.ValidateForceValueAndPosition(requestData.Forces, requestData.ForceNodePositions, degreesFreedomMaximum, response);

            this.ValidateInitialTime(requestData.InitialTime, response);

            this.ValidatePeriodDivision(requestData.PeriodDivion, response);

            this.ValidatePeriodCount(requestData.PeriodCount, response);

            this.ValidateAngularFrequency(requestData.InitialAngularFrequency, requestData.DeltaAngularFrequency, requestData.FinalAngularFrequency, response);

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

        private void ValidateInitialTime(double initialTime, OperationResponseBase response)
        {
            if(initialTime < 0)
            {
                response.AddError("014",$"Initial time: {initialTime} must be greater than or equals to zero.");
            }
        }

        private void ValidatePeriodDivision(double periodDivision, OperationResponseBase response)
        {
            if (periodDivision <= 0)
            {
                response.AddError("015", $"Period division: {periodDivision} must be greater than zero.");
            }
        }

        private void ValidatePeriodCount(double periodCount, OperationResponseBase response)
        {
            if (periodCount <= 0)
            {
                response.AddError("016", $"Period count: {periodCount} must be greater than zero.");
            }
        }

        private void ValidateInitialAngularFrequency(double initialAngularFrequency, OperationResponseBase response)
        {
            if(initialAngularFrequency < 0)
            {
                response.AddError("017", $"Initial angular frequency: {initialAngularFrequency} must be greater than or equals to zero.");
            }
        }

        private void ValidateDeltaAngularFrequency(double deltaAngularFrequency, OperationResponseBase response)
        {
            if (deltaAngularFrequency <= 0)
            {
                response.AddError("018", $"Delta angular frequency: {deltaAngularFrequency} must be greater than zero.");
            }
        }

        private void ValidateFinalAngularFrequency(double finalAngularFrequency, OperationResponseBase response)
        {
            if (finalAngularFrequency <= 0)
            {
                response.AddError("019", $"Final angular frequency: {finalAngularFrequency} must be greater than zero.");
            }
        }

        private void ValidateAngularFrequency(double initialAngularFrequency, double deltaAngularFrequency, double finalAngularFrequency, OperationResponseBase response)
        {
            this.ValidateInitialAngularFrequency(initialAngularFrequency, response);

            this.ValidateDeltaAngularFrequency(deltaAngularFrequency, response);

            this.ValidateFinalAngularFrequency(finalAngularFrequency, response);

            if(deltaAngularFrequency > finalAngularFrequency - initialAngularFrequency)
            {
                response.AddError("020",$"Delta angular frequency: {deltaAngularFrequency} must be smaller than the interval: {finalAngularFrequency - initialAngularFrequency}.");
            }
        }
    }
}
