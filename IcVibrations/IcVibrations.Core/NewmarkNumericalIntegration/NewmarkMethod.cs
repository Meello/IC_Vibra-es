using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.Beam;
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
    public abstract class NewmarkMethod : INewmarkMethod
    {
        /// <summary>
        /// Integration constants.
        /// </summary>
        private double a0, a1, a2, a3, a4, a5;
        
        private readonly ICommonMainMatrix _mainMatrix;
        private readonly IAuxiliarOperation _auxiliarMethod;
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _geometricProperty;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="mainMatrix"></param>
        /// <param name="auxiliarMethod"></param>
        /// <param name="arrayOperation"></param>
        /// <param name="geometricProperty"></param>
        public NewmarkMethod(
            ICommonMainMatrix mainMatrix,
            IAuxiliarOperation auxiliarMethod,
            IArrayOperation arrayOperation,
            ICalculateGeometricProperty geometricProperty)
        {
            _mainMatrix = mainMatrix;
            _auxiliarMethod = auxiliarMethod;
            _arrayOperation = arrayOperation;
            _geometricProperty = geometricProperty;
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
            if (input.Parameter.DeltaAngularFrequency != 0)
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
                input.AngularFrequency = input.Parameter.InitialAngularFrequency + i * input.Parameter.DeltaAngularFrequency.Value;

                var analysisResult = new Analysis()
                {
                    AngularFrequency = input.AngularFrequency,
                    Results = new List<Result>()
                };

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
                    analysisResult.Results = await Solution(input);
                }
                catch (Exception ex)
                {
                    response.AddError("000", $"Error executing the solution. {ex.Message}");

                    return null;
                }
            }

            return output;
        }

        private async Task<List<Result>> Solution(NewmarkMethodInput input)
        {
            List<Result> results = new List<Result>();

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
                        // Force can't initiate in 0?
                        input.Force[iteration] = force[iteration] * Math.Cos(input.AngularFrequency * time);
                    });

                    if (time == 0)
                    {
                        double[,] massInverse;
                        double[] matrix_K_Y;
                        double[] matrix_C_Vel;

                        //var massInverseTask = this._arrayOperation.InverseMatrix(input.Mass, input.NumberOfTrueBoundaryConditions, nameof(massInverse));
                        //var matrix_K_YTask = this._arrayOperation.Multiply(input.Hardness, y, nameof(matrix_K_Y));
                        //var matrix_C_VelTask = this._arrayOperation.Multiply(input.Damping, vel, nameof(matrix_C_Vel));

                        //massInverse = await massInverseTask;
                        //matrix_K_Y = await matrix_K_YTask;
                        //matrix_C_Vel = await matrix_C_VelTask;

                        //double[] subtractionResult = await this._arrayOperation.Subtract(input.Force, matrix_K_Y, matrix_C_Vel, $"{nameof(input.Force)}, {nameof(matrix_K_Y)}, {nameof(matrix_C_Vel)}");

                        //acel = await this._arrayOperation.Multiply(massInverse, subtractionResult, $"{nameof(massInverse)}, {nameof(subtractionResult)}");

                        var massInverseTask = Task.Run(async () =>
                        {
                            return await _arrayOperation.InverseMatrix(input.Mass, input.NumberOfTrueBoundaryConditions, nameof(massInverse)).ConfigureAwait(false);
                        });

                        var matrix_K_YTask = Task.Run(async () =>
                        {
                            return await _arrayOperation.Multiply(input.Hardness, y, nameof(matrix_K_Y)).ConfigureAwait(false);
                        });

                        var matrix_C_VelTask = Task.Run(async () =>
                        {
                            return await _arrayOperation.Multiply(input.Damping, vel, nameof(matrix_C_Vel)).ConfigureAwait(false);
                        });

                        await Task.WhenAll(massInverseTask, matrix_K_YTask, matrix_C_VelTask).ConfigureAwait(false);

                        massInverse = massInverseTask.Result;
                        matrix_K_Y = matrix_K_YTask.Result;
                        matrix_C_Vel = matrix_C_VelTask.Result;

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
                            deltaVel[iteration] = a1 * deltaY[iteration] - a3 * velAnt[iteration] - a2 * acelAnt[iteration];
                            deltaAcel[iteration] = a0 * deltaY[iteration] - a2 * velAnt[iteration] - a4 * acelAnt[iteration];

                            y[iteration] = yAnt[iteration] + deltaY[iteration];
                            vel[iteration] = velAnt[iteration] + deltaVel[iteration];
                            acel[iteration] = acelAnt[iteration] + deltaAcel[iteration];
                        });
                    }

                    if (jp >= 0)
                    {
                        Result iterationResult = new Result
                        {
                            Time = time,
                            Displacemens = y,
                            Velocities = vel,
                            Acelerations = acel,
                            Forces = input.Force
                        };

                        results.Add(iterationResult);

                        // Escrever no arquivo.
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

        private async Task<double[]> CalculateEquivalentForce(NewmarkMethodInput input, double[] force_ant, double[] vel, double[] acel)
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
