using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Methods.AuxiliarOperations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorbers.
    /// </summary>
    public class BeamWithDvaNewmarkMethod : NewmarkMethod, IBeamWithDvaNewmarkMethod
    {
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="auxiliarOperation"></param>
        public BeamWithDvaNewmarkMethod(
            IArrayOperation arrayOperation,
            IAuxiliarOperation auxiliarOperation)
            : base(arrayOperation, auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
        }

        protected override async Task<double[]> CalculateEquivalentForce(NewmarkMethodInput input, double[] displacement, double[] velocity, double[] acceleration, uint numberOfTrueBoundaryConditions)
        {
            double[] equivalentVelocity = await base.CalculateEquivalentVelocity(displacement, velocity, acceleration, numberOfTrueBoundaryConditions);
            double[] equivalentAcceleration = await base.CalculateEquivalentAcceleration(displacement, velocity, acceleration, numberOfTrueBoundaryConditions);

            double[,] mass = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];
            double[,] damping = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];
            double[] force = new double[numberOfTrueBoundaryConditions];

            for (int i = 0; i < numberOfTrueBoundaryConditions; i++)
            {
                force[i] = input.Force[i];

                for (int j = 0; j < numberOfTrueBoundaryConditions; j++)
                {
                    mass[i, j] = input.Mass[i, j];
                    damping[i, j] = input.Damping[i, j];
                }
            }

            double[] mass_accel = await this._arrayOperation.Multiply(mass, equivalentAcceleration, $"{nameof(input.Mass)} and {nameof(equivalentAcceleration)}");
            double[] damping_vel = await this._arrayOperation.Multiply(damping, equivalentVelocity, $"{nameof(input.Damping)} and {nameof(equivalentVelocity)}");

            double[] equivalentForce = await this._arrayOperation.Sum(force, mass_accel, damping_vel, $"{nameof(input.Force)}, {nameof(mass_accel)} and {nameof(damping_vel)}");

            double[] dvaForce = new double[input.NumberOfTrueBoundaryConditions];

            for (uint i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                dvaForce[i] = equivalentForce[i];
            }

            return dvaForce;
        }
    }
}
