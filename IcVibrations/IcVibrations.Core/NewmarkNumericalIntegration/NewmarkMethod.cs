using IcVibrations.Common.Classes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    /// <summary>
    /// It's responsible to execute the Newmark numerical integration method to calculate the vibration.
    /// </summary>
    public abstract class NewmarkMethod : INewmarkMethod
    {
        /// <summary>
        /// Integration constants.
        /// </summary>
        public double a0, a1, a2, a3, a4, a5, a6, a7;

        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="auxiliarOperation"></param>
        public NewmarkMethod(
            IArrayOperation arrayOperation)
        {
            this._arrayOperation = arrayOperation;
        }

        /// <summary>
        /// It's responsivel to generate the response content to Newmark integration method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<NewmarkMethodResponse> CalculateResponse(NewmarkMethodInput input, OperationResponseBase response)
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

            NewmarkMethodResponse output = new NewmarkMethodResponse
            {
                Analyses = new List<Analysis>()
            };

            for (int i = 0; i < angularFrequencyLoopCount; i++)
            {
                if (angularFrequencyLoopCount == 1)
                {
                    input.AngularFrequency = input.Parameter.InitialAngularFrequency * 2 * Math.PI;
                }
                else
                {
                    input.AngularFrequency = (input.Parameter.InitialAngularFrequency + i * input.Parameter.DeltaAngularFrequency.Value) * 2 * Math.PI;
                }

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
                    var analysisResult = new Analysis()
                    {
                        AngularFrequency = input.AngularFrequency,
                        Result = await Solution(input)
                    };

                    output.Analyses.Add(analysisResult);
                }
                catch (Exception ex)
                {
                    response.AddError("000", $"Error executing the solution. {ex.Message}");
                    throw;
                }
            }

            return output;
        }

        private async Task<Result> Solution(NewmarkMethodInput input)
        {
            uint matrixSize = input.Parameter.NumberOfPeriods * input.Parameter.PeriodDivision;
            uint line = 0;

            Result result = new Result()
            {
                Accelerations = new double[input.NumberOfTrueBoundaryConditions, matrixSize],
                Velocities = new double[input.NumberOfTrueBoundaryConditions, matrixSize],
                Displacements = new double[input.NumberOfTrueBoundaryConditions, matrixSize],
                Forces = new double[input.NumberOfTrueBoundaryConditions, matrixSize],
                Time = new double[matrixSize]
            };

            int i, jn, jp;
            double time = input.Parameter.InitialTime;

            double[] force = new double[input.NumberOfTrueBoundaryConditions];
            for (i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                force[i] = input.Force[i];
            }

            double[] y = new double[input.NumberOfTrueBoundaryConditions];
            double[] yAnt = new double[input.NumberOfTrueBoundaryConditions];
            double[] deltaY = new double[input.NumberOfTrueBoundaryConditions];

            double[] vel = new double[input.NumberOfTrueBoundaryConditions];
            double[] velAnt = new double[input.NumberOfTrueBoundaryConditions];
            double[] deltaVel = new double[input.NumberOfTrueBoundaryConditions];

            double[] accel = new double[input.NumberOfTrueBoundaryConditions];
            double[] accelAnt = new double[input.NumberOfTrueBoundaryConditions];
            double[] deltaAccel = new double[input.NumberOfTrueBoundaryConditions];

            double[] forceAnt = new double[input.NumberOfTrueBoundaryConditions];

            for (jp = 0; jp < input.Parameter.NumberOfPeriods; jp++)
            {
                for (jn = 0; jn < input.Parameter.PeriodDivision; jn++)
                {
                    Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                    {
                        // Force can't initiate in 0 (?)
                        input.Force[iteration] = force[iteration] * Math.Cos(input.AngularFrequency * time);
                    });

                    if (time != 0)
                    {
                        double[,] equivalentHardness = await CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness, input.NumberOfTrueBoundaryConditions);

                        var inversedEquivalentHardnessTask = Task.Run(async () =>
                        {
                            return await this._arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness)).ConfigureAwait(false);
                        });

                        var equivalentForceTask = Task.Run(async () =>
                        {
                            return await this.CalculateEquivalentForce(input, y, vel, accel, input.NumberOfTrueBoundaryConditions).ConfigureAwait(false);
                        });

                        await Task.WhenAll(inversedEquivalentHardnessTask, equivalentForceTask).ConfigureAwait(false);

                        double[] equivalentForce = await equivalentForceTask;
                        double[,] inversedEquivalentHardness = await inversedEquivalentHardnessTask;

                        y = await _arrayOperation.Multiply(equivalentForce, inversedEquivalentHardness, $"{nameof(equivalentForce)}, {nameof(inversedEquivalentHardness)}");

                        Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                        {
                            accel[iteration] = a0 * (y[iteration] - yAnt[iteration]) - a2 * velAnt[iteration] - a3 * accelAnt[iteration];
                            vel[iteration] = velAnt[iteration] + a6 * accelAnt[iteration] + a7 * accel[iteration];
                        });
                    }

                    Parallel.For(0, input.NumberOfTrueBoundaryConditions, iterator => 
                    {
                        result.Time[iterator] = time;
                        result.Displacements[iterator, line] = y[iterator];
                        result.Velocities[iterator, line] = vel[iterator];
                        result.Accelerations[iterator, line] = accel[iterator];
                        result.Forces[iterator, line] = force[iterator];
                    });

                    line = +1;

                    time += input.DeltaTime;

                    Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                    {
                        yAnt[iteration] = y[iteration];
                        velAnt[iteration] = vel[iteration];
                        accelAnt[iteration] = accel[iteration];
                        forceAnt[iteration] = input.Force[iteration];
                    });
                }
            }

            return result;
        }

        private async Task<double[]> CalculateEquivalentForce(NewmarkMethodInput input, double[] displacement, double[] velocity, double[] acceleration, uint numberOfTrueBoundaryConditions)
        {
            uint trueBC = numberOfTrueBoundaryConditions;

            if (displacement.Length != trueBC)
            {
                throw new Exception($"Lenth of displacement: {displacement.Length} have to be equals to number of true bondary conditions: {trueBC}.");
            }

            if (velocity.Length != trueBC)
            {
                throw new Exception($"Lenth of velocity: {velocity.Length} have to be equals to number of true bondary conditions: {trueBC}.");
            }

            if (acceleration.Length != trueBC)
            {
                throw new Exception($"Lenth of acceleration: {acceleration.Length} have to be equals to number of true bondary conditions: {trueBC}.");
            }

            if (input.Mass.Length != Math.Pow(trueBC, 2))
            {
                throw new Exception($"Lenth of input mass: {input.Mass.Length} have to be equals to number of true bondary conditions squared: {Math.Pow(trueBC, 2)}.");
            }

            if (input.Damping.Length != Math.Pow(trueBC, 2))
            {
                throw new Exception($"Lenth of input damping: {input.Damping.Length} have to be equals to number of true bondary conditions squared: {Math.Pow(trueBC, 2)}.");
            }

            if (input.Force.Length != trueBC)
            {
                throw new Exception($"Lenth of input force: {input.Force.Length} have to be equals to number of true bondary conditions: {trueBC}.");
            }

            double[] equivalentForce = new double[trueBC];

            var equivalentAccelerationTask = Task.Run(async () =>
            {
                return await this.CalculateEquivalentAcceleration(displacement, velocity, acceleration, trueBC);
            });

            var equivalentVelocityTask = Task.Run(async () =>
            {
                return await this.CalculateEquivalentVelocity(displacement, velocity, acceleration, trueBC);
            });

            await Task.WhenAll(equivalentAccelerationTask, equivalentVelocityTask).ConfigureAwait(false);

            double[] equivalentVelocity = await equivalentVelocityTask.ConfigureAwait(false);
            double[] equivalentAcceleration = await equivalentAccelerationTask.ConfigureAwait(false);

            var mass_accelTask = Task.Run(async () =>
            {
                return await this._arrayOperation.Multiply(input.Mass, equivalentAcceleration, $"{nameof(input.Mass)} and {nameof(equivalentAcceleration)}");

            });

            var damping_velTask = Task.Run(async () =>
            {
                return await this._arrayOperation.Multiply(input.Damping, equivalentVelocity, $"{nameof(input.Damping)} and {nameof(equivalentVelocity)}");
            });

            await Task.WhenAll(mass_accelTask, damping_velTask).ConfigureAwait(false);

            double[] mass_accel = await mass_accelTask;
            double[] damping_vel = await damping_velTask;

            equivalentForce = await this._arrayOperation.Sum(input.Force, mass_accel, damping_vel, $"{nameof(input.Force)}, {nameof(mass_accel)} and {nameof(damping_vel)}");

            return equivalentForce;
        }

        private Task<double[]> CalculateEquivalentAcceleration(double[] displacement, double[] velocity, double[] acceleration, uint numberOfTrueBondaryCOnditions)
        {
            double[] equivalentAcceleration = new double[numberOfTrueBondaryCOnditions];

            Parallel.For(0, numberOfTrueBondaryCOnditions, iterator =>
            {
                equivalentAcceleration[iterator] = a0 * displacement[iterator] + a2 * velocity[iterator] + a3 * acceleration[iterator];
            });

            return Task.FromResult(equivalentAcceleration);
        }

        private Task<double[]> CalculateEquivalentVelocity(double[] displacement, double[] velocity, double[] acceleration, uint numberOfTrueBondaryConditions)
        {
            double[] equivalentVelocity = new double[numberOfTrueBondaryConditions];

            Parallel.For(0, numberOfTrueBondaryConditions, iterator =>
            {
                equivalentVelocity[iterator] = a1 * displacement[iterator] + a4 * velocity[iterator] + a5 * acceleration[iterator];
            });

            return Task.FromResult(equivalentVelocity);
        }

        private Task<double[,]> CalculateEquivalentHardness(double[,] mass, double[,] damping, double[,] hardness, uint numberOfTrueBoundaryConditions)
        {
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
