using IcVibrations.Common.ErrorCodes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using IcVibrations.Methods.AuxiliarOperations;
using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    /// <summary>
    /// It's responsible to execute the Newmark numerical integration method to calculate the vibration.
    /// </summary>
    public class NewmarkMethod : INewmarkMethod
    {
        /// <summary>
        /// Integration constants.
        /// </summary>
        public double a0, a1, a2, a3, a4, a5, a6, a7;

        private readonly IArrayOperation _arrayOperation;
        private readonly IAuxiliarOperation _auxiliarOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        public NewmarkMethod(
            IArrayOperation arrayOperation,
            IAuxiliarOperation auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
            this._auxiliarOperation = auxiliarOperation;
        }

        public async Task CalculateResponse(NewmarkMethodInput input, OperationResponseBase response)
        {
            int angularFrequencyLoopCount;
            if (input.Parameter.DeltaAngularFrequency != default)
            {
                angularFrequencyLoopCount = (int)((input.Parameter.FinalAngularFrequency - input.Parameter.InitialAngularFrequency) / input.Parameter.DeltaAngularFrequency) + 1;
            }
            else
            {
                angularFrequencyLoopCount = 1;
            }

            //Parallel.For
            for (int i = 0; i < angularFrequencyLoopCount; i++)
            {
                input.AngularFrequency = (input.Parameter.InitialAngularFrequency + i * input.Parameter.DeltaAngularFrequency.Value);

                if (input.AngularFrequency != 0)
                {
                    input.DeltaTime = Math.PI * 2 / input.AngularFrequency / input.Parameter.PeriodDivision;
                }
                else
                {
                    input.DeltaTime = Math.PI * 2 / input.Parameter.PeriodDivision;
                }

                a0 = 1 / (Constants.Beta * Math.Pow(input.DeltaTime, 2));
                a1 = Constants.Gama / (Constants.Beta * input.DeltaTime);
                a2 = 1 / (Constants.Beta * input.DeltaTime);
                a3 = 1 / (2 * Constants.Beta) - 1;
                a4 = (Constants.Gama / Constants.Beta) - 1;
                a5 = (input.DeltaTime / 2) * ((Constants.Beta / Constants.Beta) - 2);
                a6 = input.DeltaTime * (1 - Constants.Gama);
                a7 = Constants.Gama * input.DeltaTime;

                try
                {
                    this._auxiliarOperation.WriteInFile(input.AngularFrequency);
                    await Solution(input);
                }
                catch (Exception ex)
                {
                    response.AddError(ErrorCode.NewmarkMethod, $"Error executing the solution. {ex.Message}");
                    return;
                }
            }
        }

        protected async Task Solution(NewmarkMethodInput input)
        {
            double time = input.Parameter.InitialTime;

            double[] force = new double[input.NumberOfTrueBoundaryConditions];
            for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                force[i] = input.Force[i];
            }

            double[] y = new double[input.NumberOfTrueBoundaryConditions];
            double[] yPre = new double[input.NumberOfTrueBoundaryConditions];

            double[] vel = new double[input.NumberOfTrueBoundaryConditions];
            double[] velPre = new double[input.NumberOfTrueBoundaryConditions];

            double[] accel = new double[input.NumberOfTrueBoundaryConditions];
            double[] accelPre = new double[input.NumberOfTrueBoundaryConditions];

            double[] forcePre = new double[input.NumberOfTrueBoundaryConditions];

            for (int jp = 0; jp < input.Parameter.NumberOfPeriods; jp++)
            {
                for (int jn = 0; jn < input.Parameter.PeriodDivision; jn++)
                {
                    for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
                    {
                        // Force can't initiate in 0 (?)
                        input.Force[i] = force[i] * Math.Cos(input.AngularFrequency * time);
                    }

                    if (time != 0)
                    {
                        y = await CalculateDisplacement(input, yPre, velPre, accelPre);

                        for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
                        {
                            accel[i] = a0 * (y[i] - yPre[i]) - a2 * velPre[i] - a3 * accelPre[i];
                            vel[i] = velPre[i] + a6 * accelPre[i] + a7 * accel[i];
                        }
                    }

                    //this._auxiliarOperation.WriteInFile(time, y);

                    time += input.DeltaTime;

                    for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
                    {
                        yPre[i] = y[i];
                        velPre[i] = vel[i];
                        accelPre[i] = accel[i];
                        forcePre[i] = input.Force[i];
                    }
                }
            }
        }

        protected async Task<double[]> CalculateDisplacement(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            if (previousDisplacement.Length != input.NumberOfTrueBoundaryConditions)
            {
                throw new Exception($"Lenth of displacement: {previousDisplacement.Length} have to be equals to number of true bondary conditions: {input.NumberOfTrueBoundaryConditions}.");
            }

            if (previousVelocity.Length != input.NumberOfTrueBoundaryConditions)
            {
                throw new Exception($"Lenth of velocity: {previousVelocity.Length} have to be equals to number of true bondary conditions: {input.NumberOfTrueBoundaryConditions}.");
            }

            if (previousAcceleration.Length != input.NumberOfTrueBoundaryConditions)
            {
                throw new Exception($"Lenth of acceleration: {previousAcceleration.Length} have to be equals to number of true bondary conditions: {input.NumberOfTrueBoundaryConditions}.");
            }

            double[,] equivalentHardness = await this.CalculateEquivalentHardness(input.Mass, input.Hardness, input.Damping, input.NumberOfTrueBoundaryConditions).ConfigureAwait(false);
            double[,] inversedEquivalentHardness = await this._arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness)).ConfigureAwait(false);

            double[] equivalentForce = await this.CalculateEquivalentForce(input, previousDisplacement, previousVelocity, previousAcceleration).ConfigureAwait(false);

            return await this._arrayOperation.Multiply(equivalentForce, inversedEquivalentHardness, $"{nameof(equivalentForce)}, {nameof(inversedEquivalentHardness)}");
        }

        /// <summary>
        /// Calculates the equivalent force to calculate the displacement in Newmark method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="previousDisplacement"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="previousAcceleration"></param>
        /// <returns></returns>
        protected virtual async Task<double[]> CalculateEquivalentForce(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            int massRow = input.Mass.GetLength(0);
            int massColumn = input.Mass.GetLength(1);
            int massLength = input.Mass.Length;
            int dampingRow = input.Damping.GetLength(0);
            int dampingColumn = input.Damping.GetLength(1);
            int dampingLength = input.Damping.Length;
            int forceLength = input.Force.Length;

            if (massRow != massColumn)
            {
                throw new IndexOutOfRangeException($"Mass matrix must be a square matrix. Sizes: {massRow}x{massColumn}.");
            }

            if (massRow < input.NumberOfTrueBoundaryConditions || massColumn < input.NumberOfTrueBoundaryConditions)
            {
                throw new IndexOutOfRangeException($"Sizes of mass matrix must be at least equals to {input.NumberOfTrueBoundaryConditions }. Mass sizes: {massRow}x{massColumn}.");
            }

            if (dampingRow != dampingColumn)
            {
                throw new IndexOutOfRangeException($"Damping matrix must be a square matrix. Sizes: {dampingRow}x{dampingColumn}.");
            }

            if (dampingRow < input.NumberOfTrueBoundaryConditions || dampingColumn < input.NumberOfTrueBoundaryConditions)
            {
                throw new IndexOutOfRangeException($"Sizes of damping matrix must be at least equals to {input.NumberOfTrueBoundaryConditions }. Damping sizes: {dampingRow}x{dampingColumn}.");
            }

            if (forceLength < input.NumberOfTrueBoundaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of force vector must be at least equals to {input.NumberOfTrueBoundaryConditions}.");
            }
            
            if (massLength != dampingLength)
            {
                throw new IndexOutOfRangeException($"Length of mass: {massLength} and damping: {dampingLength} must be equals.");
            }

            double[] equivalentVelocity = await this.CalculateEquivalentVelocity(previousDisplacement, previousVelocity, previousAcceleration, input.NumberOfTrueBoundaryConditions);
            double[] equivalentAcceleration = await this.CalculateEquivalentAcceleration(previousDisplacement, previousVelocity, previousAcceleration, input.NumberOfTrueBoundaryConditions);

            double[] mass_accel = await this._arrayOperation.Multiply(input.Mass, equivalentAcceleration, $"{nameof(input.Mass)} and {nameof(equivalentAcceleration)}");
            double[] damping_vel = await this._arrayOperation.Multiply(input.Damping, equivalentVelocity, $"{nameof(input.Damping)} and {nameof(equivalentVelocity)}");

            double[] equivalentForce = await this._arrayOperation.Sum(input.Force, mass_accel, damping_vel, $"{nameof(input.Force)}, {nameof(mass_accel)} and {nameof(damping_vel)}");

            return equivalentForce;
        }

        /// <summary>
        /// Calculates the equivalent aceleration to calculate the equivalent force.
        /// </summary>
        /// <param name="displacement"></param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="numberOfTrueBondaryConditions"></param>
        /// <returns></returns>
        protected Task<double[]> CalculateEquivalentAcceleration(double[] displacement, double[] velocity, double[] acceleration, uint numberOfTrueBondaryConditions)
        {
            int displacementLength = displacement.Length;
            int velocityLength = velocity.Length;
            int accelerationLength = acceleration.Length;

            if (displacementLength != numberOfTrueBondaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of displacement must be equals to {numberOfTrueBondaryConditions}.");
            }

            if (velocityLength != numberOfTrueBondaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of velocity must be equals to {numberOfTrueBondaryConditions}.");
            }

            if (accelerationLength != numberOfTrueBondaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of acceleration must be equals to {numberOfTrueBondaryConditions}.");
            }

            double[] equivalentAcceleration = new double[numberOfTrueBondaryConditions];

            for (int i = 0; i < numberOfTrueBondaryConditions; i++)
            {
                equivalentAcceleration[i] = a0 * displacement[i] + a2 * velocity[i] + a3 * acceleration[i];
            }

            return Task.FromResult(equivalentAcceleration);
        }

        /// <summary>
        /// Calculates the equivalent velocity to calculate the equivalent force.
        /// </summary>
        /// <param name="displacement"></param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="numberOfTrueBondaryConditions"></param>
        /// <returns></returns>
        protected Task<double[]> CalculateEquivalentVelocity(double[] displacement, double[] velocity, double[] acceleration, uint numberOfTrueBondaryConditions)
        {
            int displacementLength = displacement.Length;
            int velocityLength = velocity.Length;
            int accelerationLength = acceleration.Length;

            if (displacementLength != numberOfTrueBondaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of displacement must be equals to {numberOfTrueBondaryConditions}.");
            }

            if (velocityLength != numberOfTrueBondaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of velocity must be equals to {numberOfTrueBondaryConditions}.");
            }

            if (accelerationLength != numberOfTrueBondaryConditions)
            {
                throw new IndexOutOfRangeException($"Size of acceleration must be equals to {numberOfTrueBondaryConditions}.");
            }

            double[] equivalentVelocity = new double[numberOfTrueBondaryConditions];

            for (int i = 0; i < numberOfTrueBondaryConditions; i++)
            {
                equivalentVelocity[i] = a1 * displacement[i] + a4 * velocity[i] + a5 * acceleration[i];
            }

            return Task.FromResult(equivalentVelocity);
        }

        /// <summary>
        /// Calculates the equivalent hardness to calculate the displacement in Newmark method.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="damping"></param>
        /// <param name="hardness"></param>
        /// <param name="numberOfTrueBoundaryConditions"></param>
        /// <returns></returns>
        protected Task<double[,]> CalculateEquivalentHardness(double[,] mass, double[,] hardness, double[,] damping, uint numberOfTrueBoundaryConditions)
        {
            int massRow = mass.GetLength(0);
            int massColumn = mass.GetLength(1);
            int massLength = mass.Length;
            int hardnessRow = hardness.GetLength(0);
            int hardnessColumn = hardness.GetLength(1);
            int hardnessLength = hardness.Length;
            int dampingRow = damping.GetLength(0);
            int dampingColumn = damping.GetLength(1);
            int dampingLength = damping.Length;

            if(massRow != massColumn)
            {
                throw new IndexOutOfRangeException($"Mass matrix must be a square matrix. Sizes: {massRow}x{massColumn}.");
            }

            if (massRow < numberOfTrueBoundaryConditions || massColumn < numberOfTrueBoundaryConditions)
            {
                throw new IndexOutOfRangeException($"Sizes of mass matrix must be at least equals to {numberOfTrueBoundaryConditions}. Mass sizes: {massRow}x{massColumn}.");
            }

            if (hardnessRow != hardnessColumn)
            {
                throw new IndexOutOfRangeException($"Hardness matrix must be a square matrix. Sizes: {hardnessRow}x{hardnessColumn}.");
            }

            if (hardnessRow < numberOfTrueBoundaryConditions || hardnessColumn < numberOfTrueBoundaryConditions)
            {
                throw new IndexOutOfRangeException($"Sizes of hardness matrix must be at least equals to {numberOfTrueBoundaryConditions}. Hardness sizes: {hardnessRow}x{hardnessColumn}.");
            }

            if (dampingRow != dampingColumn)
            {
                throw new IndexOutOfRangeException($"Damping matrix must be a square matrix. Sizes: {dampingRow}x{dampingColumn}.");
            }

            if (dampingRow < numberOfTrueBoundaryConditions || dampingColumn < numberOfTrueBoundaryConditions)
            {
                throw new IndexOutOfRangeException($"Sizes of damping matrix must be at least equals to {numberOfTrueBoundaryConditions}. Damping sizes: {dampingRow}x{dampingColumn}.");
            }

            if (massLength != hardnessLength || massLength != dampingLength || hardnessLength != dampingLength)
            {
                throw new IndexOutOfRangeException($"Length of mass: {massLength}, hardness: {hardnessLength} and damping: {dampingLength} must be equal.");
            }

            double[,] equivalentHardness = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];

            for (int i = 0; i < numberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < numberOfTrueBoundaryConditions; j++)
                {
                    equivalentHardness[i, j] = a0 * mass[i, j] + a1 * damping[i, j] + hardness[i, j];
                }
            }

            return Task.FromResult(equivalentHardness);
        }
    }
}
