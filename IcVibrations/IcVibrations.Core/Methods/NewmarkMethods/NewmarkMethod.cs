using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IcVibrations.Methods.NewmarkMethod
{
    public class NewmarkMethod : INewmarkMethod
    {
        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarOperation _auxiliarMethod;
        private readonly IArrayOperation _arrayOperation;
        private readonly IGeometricProperty _geometricProperty;

        public NewmarkMethod(
            IMainMatrix mainMatrix,
            IAuxiliarOperation auxiliarMethod,
            IArrayOperation arrayOperation,
            IGeometricProperty geometricProperty)
        {
            this._mainMatrix = mainMatrix;
            this._auxiliarMethod = auxiliarMethod;
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
        }

        public async Task<NewmarkMethodOutput> CreateOutput(NewmarkMethodInput input, OperationResponseBase response)
        {
            int angularFrequencyLoopCount;
            if (input.DeltaAngularFrequency != 0)
            {
                angularFrequencyLoopCount = (int)((input.FinalAngularFrequency - input.InitialAngularFrequency) / input.DeltaAngularFrequency) + 1;
            }
            else
            {
                angularFrequencyLoopCount = 1;

            }

            NewmarkMethodOutput output = new NewmarkMethodOutput
            {
                Analyses = new List<Analysis>()
            };

            for (int i = 0; i < angularFrequencyLoopCount; i++)
            {
                input.AngularFrequency = input.InitialAngularFrequency + (i * input.DeltaAngularFrequency);

                var analysisResult = new Analysis()
                {
                    AngularFrequency = input.AngularFrequency,
                    Results = new List<Result>()
                };

                if (input.AngularFrequency != 0)
                {
                    input.DeltaTime = (Math.PI * 2 / input.AngularFrequency) / input.NumberOfPeriodDivisions;
                }
                else
                {
                    input.DeltaTime = (Math.PI * 2) / input.NumberOfPeriodDivisions;
                }

                input.input.A0 = 1 / (Constants.Beta * Math.Pow(input.DeltaTime, 2));
                input.A1 = Constants.Gama / (Constants.Beta * input.DeltaTime);
                a2 = 1.0 / (Constants.Beta * input.DeltaTime);
                a3 = Constants.Gama / (Constants.Beta);
                a4 = 1 / (2 * Constants.Beta);
                a5 = input.DeltaTime * ((Constants.Gama / (2 * Constants.Beta)) - 1);

                try
                {
                    analysisResult.Results = await this.Solution(input);
                }
                catch (Exception ex)
                {
                    response.AddError("000", $"Error executing the solution. {ex.Message}");

                    return null;
                }
            }

            return output;
        }

        public async Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, RectangularBeam beam, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            uint piezoelectricDegreesFreedomMaximum = beam.ElementCount + 1;

            // Calculate geometric properties
            double beamArea = this._geometricProperty.Area(beam.Height, beam.Width, beam.Thickness);

            double beamMomentInertia = this._geometricProperty.MomentInertia(beam.Height, beam.Width, beam.Thickness);

            double piezoelectricArea = this._geometricProperty.Area(piezoelectric.Height, piezoelectric.Width, piezoelectric.Thickness);

            double piezoelectricMomentInertia = this._geometricProperty.MomentInertia(piezoelectric.Height, piezoelectric.Width, piezoelectric.Thickness);

            // Create geometric properties matrixes
            var beamAreaTask = this._arrayOperation.Create(beamArea, beam.ElementCount);
            var beamMomentOfInertiaTask = this._arrayOperation.Create(beamMomentInertia, beam.ElementCount);
            var piezoelectricAreaTask = this._arrayOperation.Create(piezoelectricArea, beam.ElementCount, piezoelectric.ElementsWithPiezoelectric, $"{nameof(piezoelectric)} {nameof(piezoelectric.GeometricProperty.Area)}");
            var piezoelectricMomentOfInertiaTask = this._arrayOperation.Create(piezoelectricMomentInertia, beam.ElementCount, piezoelectric.ElementsWithPiezoelectric, $"{nameof(piezoelectric)} {nameof(piezoelectric.GeometricProperty.MomentOfInertia)}");

            beam.GeometricProperty.Area = await beamAreaTask;
            beam.GeometricProperty.MomentOfInertia = await beamMomentOfInertiaTask;
            piezoelectric.GeometricProperty.Area = await piezoelectricAreaTask;
            piezoelectric.GeometricProperty.MomentOfInertia = await piezoelectricMomentOfInertiaTask;

            // Calculate basic matrix
            double[,] mass = await this._mainMatrix.CalculateMass(beam, piezoelectric, degreesFreedomMaximum);

            double[,] hardness = await this._mainMatrix.CalculateHardness(beam, piezoelectric, degreesFreedomMaximum);

            double[,] piezoelectricElectromechanicalCoupling = await this._mainMatrix.CalculatePiezoelectricElectromechanicalCoupling(beam.Height, beam.ElementCount, piezoelectric, degreesFreedomMaximum);

            double[,] piezoelectricCapacitance = await this._mainMatrix.CalculatePiezoelectricCapacitance(piezoelectric, beam.ElementCount);

            // Calculate input
            input.Mass = await this._mainMatrix.CalculateEquivalentMass(mass, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);

            input.Hardness = await this._mainMatrix.CalculateEquivalentHardness(hardness, piezoelectricElectromechanicalCoupling, piezoelectricCapacitance, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);

            input.Damping = await this._mainMatrix.CalculateDamping(input.Mass, input.Hardness, degreesFreedomMaximum);

            input.Force = beam.Forces;

            return input;
        }

        public async Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, BeamWithDva beam, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = await this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = await this._mainMatrix.CalculateBeamHardness(beam, degreesFreedomMaximum);

            double[,] massWithDva = await this._mainMatrix.CalculateMassWithDva(mass, beam.DvaMasses, beam.DvaNodePositions);

            double[,] hardnessWithDva = await this._mainMatrix.CalculateBeamHardnessWithDva(hardness, beam.DvaHardnesses, beam.DvaNodePositions);

            double[,] dampingWithDva = await this._mainMatrix.CalculateDamping(massWithDva, hardnessWithDva, degreesFreedomMaximum);

            double[] forces = beam.Forces;

            bool[] bondaryCondition = await this._mainMatrix.CalculateBeamBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    input.NumberOfTrueBoundaryConditions += 1;
                }
            }

            // Output values
            input.Mass = this._auxiliarMethod.AplyBondaryConditions(massWithDva, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardnessWithDva, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(dampingWithDva, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(forces, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.InitialTime = newmarkMethodParameter.InitialTime;

            input.NumberOfPeriodDivisions = newmarkMethodParameter.PeriodDivion;

            input.Period = newmarkMethodParameter.NumberOfPeriods;

            wi = newmarkMethodParameter.InitialAngularFrequency;

            dw = newmarkMethodParameter.DeltaAngularFrequency.Value;

            wf = newmarkMethodParameter.FinalAngularFrequency;

            return input;
        }

        public async Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, Beam beam, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = await this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = await this._mainMatrix.CalculateBeamHardness(beam, degreesFreedomMaximum);

            double[,] damping = await this._mainMatrix.CalculateDamping(mass, hardness, degreesFreedomMaximum);

            double[] forces = beam.Forces;

            bool[] bondaryCondition = await this._mainMatrix.CalculateBeamBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            input.NumberOfTrueBoundaryConditions = 0;
            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    input.NumberOfTrueBoundaryConditions += 1;
                }
            }

            // Output values
            input.Mass = this._auxiliarMethod.AplyBondaryConditions(mass, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardness, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(damping, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(forces, bondaryCondition, input.NumberOfTrueBoundaryConditions);

            input.InitialTime = newmarkMethodParameter.InitialTime;

            input.NumberOfPeriodDivisions = newmarkMethodParameter.PeriodDivion;

            pC = newmarkMethodParameter.NumberOfPeriods;

            wi = newmarkMethodParameter.InitialAngularFrequency;

            dw = newmarkMethodParameter.DeltaAngularFrequency.Value;

            wf = newmarkMethodParameter.FinalAngularFrequency;

            return input;
        }

        public async Task<List<Result>> Solution(NewmarkMethodInput input)
        {
            List<Result> results = new List<Result>();

            int i, jn, jp;
            double time = input.InitialTime;

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

            for (jp = 0; jp < pC; jp++)
            {
                for (jn = 0; jn < input.NumberOfPeriodDivisions; jn++)
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
                            return await this._arrayOperation.InverseMatrix(input.Mass, input.NumberOfTrueBoundaryConditions, nameof(massInverse)).ConfigureAwait(false);
                        });

                        var matrix_K_YTask = Task.Run(async () =>
                        {
                            return await this._arrayOperation.Multiply(input.Hardness, y, nameof(matrix_K_Y)).ConfigureAwait(false);
                        });

                        var matrix_C_VelTask = Task.Run(async () =>
                        {
                            return await this._arrayOperation.Multiply(input.Damping, vel, nameof(matrix_C_Vel)).ConfigureAwait(false);
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
                        double[,] equivalentHardness = await this.CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness);

                        var inversedEquivalentHardnessTask = this._arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness));
                        var equivalentForceTask = this.CalculateEquivalentForce(input, forceAnt, vel, acel);

                        double[] equivalentForce = await equivalentForceTask;
                        double[,] inversedEquivalentHardness = await inversedEquivalentHardnessTask;

                        deltaY = await this._arrayOperation.Multiply(equivalentForce, inversedEquivalentHardness, $"{nameof(equivalentForce)}, {nameof(inversedEquivalentHardness)}");

                        Parallel.For(0, input.NumberOfTrueBoundaryConditions, iteration =>
                        {
                            deltaVel[iteration] = input.A1 * deltaY[iteration] - input.A3 * velAnt[iteration] - input.A2 * acelAnt[iteration];
                            deltaAcel[iteration] = input.A0 * deltaY[iteration] - input.A2 * velAnt[iteration] - input.A4 * acelAnt[iteration];

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

        private Task<double[,]> CalculateEquivalentHardness(double[,] mass, double[,] damping, double[,] hardness)
        {
            double[,] equivalentHardness = new double[input.NumberOfTrueBoundaryConditions, input.NumberOfTrueBoundaryConditions];

            for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfTrueBoundaryConditions; j++)
                {
                    equivalentHardness[i, j] = input.A0 * mass[i, j] + input.A1 * damping[i, j] + hardness[i, j];
                }
            }

            return Task.FromResult(equivalentHardness);
        }

        private Task<double[,]> CalculateMatrixP1(double[,] mass, double[,] damping)
        {
            double[,] p1 = new double[input.NumberOfTrueBoundaryConditions, input.NumberOfTrueBoundaryConditions];

            for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfTrueBoundaryConditions; j++)
                {
                    p1[i, j] = a2 * mass[i, j] + a3 * damping[i, j];
                }
            }

            return Task.FromResult(p1);
        }

        private Task<double[,]> CalculateMatrixP2(double[,] mass, double[,] damping)
        {
            double[,] p2 = new double[input.NumberOfTrueBoundaryConditions, input.NumberOfTrueBoundaryConditions];

            for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfTrueBoundaryConditions; j++)
                {
                    p2[i, j] = a4 * mass[j, i] + a5 * damping[i, j];
                }
            }

            return Task.FromResult(p2);
        }

        private async Task<double[]> CalculateEquivalentForce(NewmarkMethodInput input, double[] force_ant, double[] vel, double[] acel)
        {
            var deltaForceTask = this._arrayOperation.Subtract(input.Force, force_ant, $"{nameof(input.Force)}, {nameof(force_ant)}");
            var p1Task = this.CalculateMatrixP1(input.Mass, input.Damping);
            var p2Task = this.CalculateMatrixP2(input.Mass, input.Damping);
            
            double[,] p1 = await p1Task;
            double[,] p2 = await p2Task;
            
            var vel_p1Task = this._arrayOperation.Multiply(p1, vel, $"{nameof(p1)}, {nameof(vel)}");
            var acel_p2Task = this._arrayOperation.Multiply(p2, acel, $"{nameof(p2)}, {nameof(acel)}");

            double[] vel_p1 = await vel_p1Task;
            double[] acel_p2 = await acel_p2Task;
            double[] deltaForce = await deltaForceTask;

            double[] equivalentForce = await this._arrayOperation.Sum(deltaForce, vel_p1, acel_p2, $"{nameof(deltaForce)}, {nameof(vel_p1)}, {nameof(acel_p2)}");

            return equivalentForce;
        }
    }
}
