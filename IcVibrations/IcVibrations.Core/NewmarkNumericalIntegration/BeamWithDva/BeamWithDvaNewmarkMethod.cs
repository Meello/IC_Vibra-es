using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Methods.AuxiliarOperations;
using System;
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

        protected override async Task<double[]> CalculateEquivalentForce(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration, uint numberOfTrueBoundaryConditions)
        {
            if (previousDisplacement.Length != numberOfTrueBoundaryConditions)
            {
                throw new Exception($"Lenth of displacement: {previousDisplacement.Length} have to be equals to number of true bondary conditions: {numberOfTrueBoundaryConditions}.");
            }

            if (previousVelocity.Length != numberOfTrueBoundaryConditions)
            {
                throw new Exception($"Lenth of velocity: {previousVelocity.Length} have to be equals to number of true bondary conditions: {numberOfTrueBoundaryConditions}.");
            }

            if (previousAcceleration.Length != numberOfTrueBoundaryConditions)
            {
                throw new Exception($"Lenth of acceleration: {previousAcceleration.Length} have to be equals to number of true bondary conditions: {numberOfTrueBoundaryConditions}.");
            }

            double[] equivalentVelocity = await base.CalculateEquivalentVelocity(previousDisplacement, previousVelocity, previousAcceleration, numberOfTrueBoundaryConditions);
            double[] equivalentAcceleration = await base.CalculateEquivalentAcceleration(previousDisplacement, previousVelocity, previousAcceleration, numberOfTrueBoundaryConditions);

            double[,] mass = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];
            double[,] damping = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];

            for (int i = 0; i < numberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < numberOfTrueBoundaryConditions; j++)
                {
                    mass[i, j] = input.Mass[i, j];
                    damping[i, j] = input.Damping[i, j];
                }
            }

            double[] mass_accel = await this._arrayOperation.Multiply(mass, equivalentAcceleration, $"{nameof(input.Mass)} and {nameof(equivalentAcceleration)}");
            double[] damping_vel = await this._arrayOperation.Multiply(damping, equivalentVelocity, $"{nameof(input.Damping)} and {nameof(equivalentVelocity)}");

            double[] equivalentForce = await this._arrayOperation.Sum(input.Force, mass_accel, damping_vel, $"{nameof(input.Force)}, {nameof(mass_accel)} and {nameof(damping_vel)}");

            return equivalentForce;
        }
    }
}
