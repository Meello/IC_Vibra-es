using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Methods.AuxiliarMethods;
using IcVibrations.Models.Beam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IcVibrations.Methods.NewmarkMethod
{
    public class NewmarkMethod : INewmarkMethod
    {
        // Parameters to Newmark Method
        private double a0, a1, a2, a3, a4, a5, a6, a7;
        // Degrees freedom maximum
        private double glMax;
        // Boundary condition true
        private int bcTrue;
        // Angular frequency 
        private double wf, wi, dw, w;
        // Time
        private double dt, t0, pD, pC;

        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarMethod _auxiliarMethod;
        private readonly IArrayOperation _arrayOperation;

        public NewmarkMethod(
            IMainMatrix mainMatrix,
            IAuxiliarMethod auxiliarMethod,
            IArrayOperation arrayOperation)
        {
            this._mainMatrix = mainMatrix;
            this._auxiliarMethod = auxiliarMethod;
            this._arrayOperation = arrayOperation;
        }

        public NewmarkMethodOutput CreateOutput(NewmarkMethodInput input, OperationResponseBase response)
        {
            NewmarkMethodOutput output = new NewmarkMethodOutput();

            int angularFrequencyLoopCount = (int)((wf - wi) / dw) + 1;

            for (int i = 0; i < angularFrequencyLoopCount; i++)
            {
                w = wi + (i * dw);

                if (w != 0)
                {
                    dt = (Math.PI * 2 / w) / pD;
                }
                else
                { 
                    dt = (Math.PI * 2) / pD;
                }

                a0 = 1 / (Constants.Beta * Math.Pow(dt, 2));
                a1 = Constants.Gama / (Constants.Beta * dt);
                a2 = 1.0 / (Constants.Beta * dt);
                a3 = Constants.Gama / (Constants.Beta);
                a4 = 1 / (2 * Constants.Beta);
                a5 = dt * ((Constants.Gama / (2 * Constants.Beta)) - 1);
                a6 = dt * (1 - Constants.Gama);
                a7 = Constants.Gama * dt;

                output = this.Solution(input, response);
            }

            return output;
        }

        public NewmarkMethodInput CreateInput(BeamRequestData requestData, Beam beam, int degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = this._mainMatrix.BuildMass(beam, degreesFreedomMaximum, requestData.ElementCount);

            double[,] hardness = this._mainMatrix.BuildHardness(beam, degreesFreedomMaximum, requestData.ElementCount);

            double[,] damping = this._mainMatrix.BuildDamping(mass, hardness, degreesFreedomMaximum);

            double[] force = this._mainMatrix.BuildForce(requestData.Forces, requestData.ForceNodePositions, degreesFreedomMaximum);

            bool[] bondaryCondition = this._mainMatrix.BuildBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            bcTrue = 0;
            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    bcTrue += 1;
                }
            }

            // Output values
            glMax = degreesFreedomMaximum;
            
            input.Mass = this._auxiliarMethod.AplyBondaryConditions(mass, bondaryCondition);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardness, bondaryCondition);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(damping, bondaryCondition);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(force, bondaryCondition);

            t0 = requestData.InitialTime;

            pD = requestData.PeriodDivion;
            
            pC = requestData.PeriodCount;
            
            wi = requestData.InitialAngularFrequency;
            
            dw = requestData.DeltaAngularFrequency;
            
            wf = requestData.FinalAngularFrequency;

            return input;
        }

        private NewmarkMethodOutput Solution(NewmarkMethodInput input, OperationResponseBase response)
        {
            NewmarkMethodOutput output = new NewmarkMethodOutput();

            StreamWriter streamWriter = new StreamWriter(@"C:\Workspace\IC VIbrações\IcVibrations\Solutions\RectangularBeamSolution.csv");

            int i, jn, jp;
            double time = 0;

            double[] y = new double[bcTrue];
            double[] y_ant = new double[bcTrue];
            double[] delta_y = new double[bcTrue];

            double[] vel = new double[bcTrue];
            double[] vel_ant = new double[bcTrue];
            double[] delta_vel = new double[bcTrue];

            double[] acel = new double[bcTrue];
            double[] acel_ant = new double[bcTrue];
            double[] delta_acel = new double[bcTrue];

            double[] force_ant = new double[bcTrue];

            for (jp = 1; jp <= pC - 1; jp++)
            {
                for (jn = 1; jn <= pD - 1; jn++)            
                {
                    for (i = 0; i < bcTrue; i++)
                    {
                        input.Force[i] = input.Force[i] * Math.Sin(w * time);
                    }

                    if (time == 0)
                    {
                        double[,] massInverse = this._arrayOperation.InverseMatrix(input.Mass);

                        double[] matrix_K_C_Y = this.CreateMatrix_K_C_Y(input, y, response);

                        acel = this._arrayOperation.Multiply(massInverse, matrix_K_C_Y);

                        for (i = 0; i < bcTrue; i++)
                        {
                            acel_ant[i] = y[i];                
                        }
                    }

                    if (jp > 0)
                    {
                        if (Math.Sqrt((w - 0.5) * (w - 0.5)) < 0.0001)
                        {
                            output.Result.Add(y);
                            output.Time.Add(time);

                            try
                            {
                                using (StreamWriter sw = streamWriter)
                                {
                                    sw.WriteLine(string.Format("{0},{1},{2},{3}", w, time, y, vel, acel, input.Force));

                                    sw.Close();
                                }
                            }
                            catch
                            {
                                // Não quero que pare, só avise que deu erro.
                                throw new Exception("Couldn't ope file.");
                            }
                        }
                    }

                    double[,] equivalentHardness = this.BuildEquivalentHardness(input.Mass, input.Damping, input.Hardness);
                    double[] equivalentForce = this.BuildEquivalentForce(input, force_ant, vel, acel);

                    double[,] equivalentHardnessInverse = this._arrayOperation.InverseMatrix(equivalentHardness);

                    delta_y = this._arrayOperation.Multiply(equivalentForce,equivalentHardnessInverse);

                    for (i = 0; i < bcTrue; i++)
                    {
                        delta_vel[i] = a1 * delta_y[i] - a3 * vel_ant[i] - a5 * acel_ant[i];
                    }

                    for (i = 0; i < bcTrue; i++)
                    {
                        delta_y[i + bcTrue] = a0 * delta_y[i] - a2 * vel_ant[i] - a4 * acel_ant[i];
                    }

                    for (i = 0; i < bcTrue; i++)
                    {
                        y[i] = y_ant[i] + delta_y[i];
                        vel[i] = vel_ant[i] + delta_vel[i];
                        acel[i] = acel_ant[i] + delta_acel[i];
                    }

                    time += dt;

                    for (i = 0; i < 3 * bcTrue; i++)
                    {
                        y_ant[i] = y[i];
                    }

                    for (i = 0; i < bcTrue; i++)
                    {
                        force_ant[i] = input.Force[i];
                    }
                }
            }

            return output;
        }

        private double[,] BuildEquivalentHardness(double[,] mass, double[,] damping, double[,] hardness)
        {
            double[,] equivalentHardness = new double[bcTrue, bcTrue];

            /* montagem da matriz massa*/
            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    equivalentHardness[i,j] = a0 * mass[i,j] + a1 * damping[i,j] + hardness[i,j];
                }
            }

            return equivalentHardness;
        }


        private double[,] BuildMatrixP1(double[,] mass, double[,] damping)
        {
            double[,] p1 = new double[bcTrue, bcTrue];

            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    p1[i,j] = a2 * mass[i,j] + a3 * damping[i,j];
                }
            }

            return p1;
        }

        private double[,] BuildMatrixP2(double[,] mass, double[,] damping)
        {
            double[,] p2 = new double[bcTrue, bcTrue];

            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    p2[i, j] = a4 * mass[j, i] + a5 * damping[i, j];
                }
            }

            return p2;
        }

        private double[] BuildEquivalentForce(NewmarkMethodInput input, double[] force_ant, double[] vel, double[] acel)
        {
            double[] deltaForce = this._arrayOperation.Subtract(input.Force, force_ant);

            double[,] p1 = this.BuildMatrixP1(input.Mass, input.Damping);

            double[,] p2 = this.BuildMatrixP2(input.Mass, input.Damping);

            double[] vel_p1 = this._arrayOperation.Multiply(p1, vel);

            double[] acel_p2 = this._arrayOperation.Multiply(p2, acel);

            double[] equivalentForce = _arrayOperation.Sum(deltaForce, _arrayOperation.Sum(vel_p1, acel_p2));

            return equivalentForce;
        }

        private double[] CreateMatrix_K_C_Y(NewmarkMethodInput input, double[] displacement, OperationResponseBase response)
        {
            double[] matrix_K_Y = this._arrayOperation.Multiply(input.Hardness, displacement);

            if(matrix_K_Y.Length != bcTrue)
            {
                response.AddError("101", $"Size of matrix HardnessXDisplacement is incorrect: {matrix_K_Y.Length}. Correct size: {bcTrue}.");

                return null;
            }

            double[] matrix_C_Vel = this._arrayOperation.Multiply(input.Damping, displacement);

            if (matrix_C_Vel.Length != bcTrue)
            {
                response.AddError("102", $"Size of matrix DampingXVelocity is incorrect: {matrix_C_Vel.Length}. Correct size: {bcTrue}.");

                return null;
            }

            double[] result = this._arrayOperation.Subtract(input.Force, this._arrayOperation.Sum(matrix_K_Y, matrix_C_Vel));

            if (result.Length != bcTrue)
            {
                response.AddError("103", $"Size of matrix K_C_Y is incorrect: {result.Length}. Correct size: {bcTrue}.");

                return null;
            }

            return result;
        }
    }
}
