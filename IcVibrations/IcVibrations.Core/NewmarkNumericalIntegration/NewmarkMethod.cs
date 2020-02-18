using Dasync.Collections;
using IcVibrations.Common.Classes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using IcVibrations.Methods.AuxiliarOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration
{
    /// <summary>
    /// It's responsible to execute the Newmark numerical integration method to calculate the vibration.
    /// </summary>
    public abstract class NewmarkMethod<TInput> : INewmarkMethod<TInput>
        where TInput : INewmarkMethodInput
    {
        /// <summary>
        /// Integration constants.
        /// </summary>
        private double a0, a1, a2, a3, a4, a5;

        /// <summary>
        /// It's responsible to calculate the aceleration in the time = 0.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="displacement"></param>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public abstract Task<double[]> CalculateAcelInTime0(TInput input);

        private readonly IArrayOperation _arrayOperation;
        private readonly IAuxiliarOperation _auxiliarOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="auxiliarOperation"></param>
        public NewmarkMethod(
            IArrayOperation arrayOperation,
            IAuxiliarOperation auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
            this._auxiliarOperation = auxiliarOperation;
        }

        /// <summary>
        /// It's responsivel to generate the response content to Newmark integration method.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public IAsyncEnumerable<Analysis> CalculateResponse(TInput input, OperationResponseBase response)
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

            return new AsyncEnumerable<Analysis>(async yield =>
            {
                int iterator = 0;
                if (angularFrequencyLoopCount == 1)
                {
                    input.AngularFrequency = input.Parameter.InitialAngularFrequency * 2 * Math.PI;
                }
                else
                {
                    input.AngularFrequency = (input.Parameter.InitialAngularFrequency + iterator * input.Parameter.DeltaAngularFrequency.Value) * 2 * Math.PI;
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
                a2 = 1.0 / (Constants.Beta * input.DeltaTime);
                a3 = Constants.Gama / Constants.Beta;
                a4 = 1 / (2 * Constants.Beta);
                a5 = input.DeltaTime * (Constants.Gama / (2 * Constants.Beta) - 1);

                try
                {
                    var analysisResult = new Analysis()
                    {
                        AngularFrequency = input.AngularFrequency,
                        Results = await Solution(input).ConfigureAwait(false)
                    };

                    iterator += 1;

                    await yield.ReturnAsync(analysisResult).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    response.AddError("000", $"Error executing the solution. {ex.Message}");
                    throw;
                }
            });
        }

        private async Task<List<Result>> Solution(TInput input)
        {
            string path = @"C:\Users\bruno.silveira\Documents\GitHub\IC_Vibrações\IcVibrations\Solutions\TestSolution.txt";

            List<Result> results = new List<Result>();
            Result result = new Result();

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

            double[] acel = new double[input.NumberOfTrueBoundaryConditions];
            double[] acelAnt = new double[input.NumberOfTrueBoundaryConditions];
            double[] deltaAcel = new double[input.NumberOfTrueBoundaryConditions];

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

                    if (time == 0)
                    {
                        acel = await this.CalculateAcelInTime0(input);

                        Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                        {
                            acelAnt[iteration] = acel[iteration];
                        });
                    }
                    else
                    {
                        double[,] equivalentHardness = await CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness, input.NumberOfTrueBoundaryConditions);

                        var inversedEquivalentHardnessTask = _arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness));
                        var equivalentForceTask = CalculateEquivalentForce(input, forceAnt, vel, acel);

                        double[] equivalentForce = await equivalentForceTask;
                        double[,] inversedEquivalentHardness = await inversedEquivalentHardnessTask;

                        deltaY = await _arrayOperation.Multiply(equivalentForce, inversedEquivalentHardness, $"{nameof(equivalentForce)}, {nameof(inversedEquivalentHardness)}");

                        Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                        {
                            deltaVel[iteration] = a1 * deltaY[iteration] - a3 * velAnt[iteration] - a5 * acelAnt[iteration];
                            deltaAcel[iteration] = a0 * deltaY[iteration] - a2 * velAnt[iteration] - a4 * acelAnt[iteration];

                            y[iteration] = yAnt[iteration] + deltaY[iteration];
                            vel[iteration] = velAnt[iteration] + deltaVel[iteration];
                            acel[iteration] = acelAnt[iteration] + deltaAcel[iteration];
                        });
                    }

                    result.Time = time;
                    result.Displacemens = y;
                    result.Velocities = vel;
                    result.Acelerations = acel;
                    result.Forces = force;

                    if (jp == 0 && jn == 0)
                    {
                        this._auxiliarOperation.WriteInFile(path, $"time, y-{input.NumberOfTrueBoundaryConditions}, vel-{input.NumberOfTrueBoundaryConditions}, acel-{input.NumberOfTrueBoundaryConditions}, force-{input.NumberOfTrueBoundaryConditions}");
                        this._auxiliarOperation.WriteInFile(path, result);
                    }
                    else
                    {
                        this._auxiliarOperation.WriteInFile(path, result);
                    }

                    time += input.DeltaTime;

                    Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                    {
                        yAnt[iteration] = y[iteration];
                        velAnt[iteration] = vel[iteration];
                        acelAnt[iteration] = acel[iteration];
                        forceAnt[iteration] = input.Force[iteration];
                    });
                }
            }

            return results;
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

        private Task<double[,]> CalculateMatrixP1(double[,] mass, double[,] damping, uint numberOfTrueBoundaryConditions)
        {
            double[,] p1 = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];

            for (int i = 0; i < numberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < numberOfTrueBoundaryConditions; j++)
                {
                    p1[i, j] = a2 * mass[i, j] + a3 * damping[i, j];
                }
            }

            return Task.FromResult(p1);
        }

        private Task<double[,]> CalculateMatrixP2(double[,] mass, double[,] damping, uint numberOfTrueBoundaryConditions)
        {
            double[,] p2 = new double[numberOfTrueBoundaryConditions, numberOfTrueBoundaryConditions];

            for (int i = 0; i < numberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < numberOfTrueBoundaryConditions; j++)
                {
                    p2[i, j] = a4 * mass[j, i] + a5 * damping[i, j];
                }
            }

            return Task.FromResult(p2);
        }

        private async Task<double[]> CalculateEquivalentForce(TInput input, double[] force_ant, double[] vel, double[] acel)
        {
            var deltaForceTask = _arrayOperation.Subtract(input.Force, force_ant, $"{nameof(input.Force)}, {nameof(force_ant)}");
            var p1Task = CalculateMatrixP1(input.Mass, input.Damping, input.NumberOfTrueBoundaryConditions);
            var p2Task = CalculateMatrixP2(input.Mass, input.Damping, input.NumberOfTrueBoundaryConditions);

            double[,] p1 = await p1Task;
            double[,] p2 = await p2Task;

            var vel_p1Task = _arrayOperation.Multiply(p1, vel, $"{nameof(p1)}, {nameof(vel)}");
            var acel_p2Task = _arrayOperation.Multiply(p2, acel, $"{nameof(p2)}, {nameof(acel)}");

            double[] vel_p1 = await vel_p1Task;
            double[] acel_p2 = await acel_p2Task;
            double[] deltaForce = await deltaForceTask;

            double[] equivalentForce = await _arrayOperation.Sum(deltaForce, vel_p1, acel_p2, $"{nameof(deltaForce)}, {nameof(vel_p1)}, {nameof(acel_p2)}");

            return equivalentForce;
        }
    }
}
