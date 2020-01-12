using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Methods.AuxiliarMethods;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public class NewmarkMethod : INewmarkMethod
    {
        private double a0, a1, a2, a3, a4, a5, a6, a7;

        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarMethod _auxiliarMethod;

        public NewmarkMethod(
            IMainMatrix mainMatrix,
            IAuxiliarMethod auxiliarMethod)
        {
            this._mainMatrix = mainMatrix;
            this._auxiliarMethod = auxiliarMethod;
        }

        public NewmarkMethodOutput CreateOutput(NewmarkMethodInput input, OperationResponseBase response)
        {
            NewmarkMethodOutput output = new NewmarkMethodOutput();

            double dt = 0;

            a0 = 1 / (Constants.Beta * Math.Pow(dt, 2));
            a1 = Constants.Gama / (Constants.Beta * dt);
            a2 = 1.0 / (Constants.Beta * dt);
            a3 = Constants.Gama / (Constants.Beta);
            a4 = 1 / (2 * Constants.Beta);
            a5 = dt * ((Constants.Gama / (2 * Constants.Beta)) - 1);
            a6 = dt * (1 - Constants.Gama);
            a7 = Constants.Gama * dt;

            return output;
        }

        public NewmarkMethodInput CreateInput(BeamRequestData requestData, Beam beam, int degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = this._mainMatrix.CreateMass(beam, degreesFreedomMaximum, requestData.ElementCount);

            double[,] hardness = this._mainMatrix.CreateHardness(beam, degreesFreedomMaximum, requestData.ElementCount);

            double[,] damping = this._mainMatrix.CreateDamping(input.Mass, input.Hardness, degreesFreedomMaximum);

            double[] force = this._mainMatrix.CreateForce(requestData.Forces, requestData.ForceNodePositions, degreesFreedomMaximum);

            bool[] bondaryCondition = this._mainMatrix.CreateBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            // Output values
            input.Mass = this._auxiliarMethod.AplyBondaryConditions(mass, bondaryCondition);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardness, bondaryCondition);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(damping, bondaryCondition);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(force, bondaryCondition);

            input.DegreesFreedomMaximum = degreesFreedomMaximum;

            return input;
        }
    }
}
