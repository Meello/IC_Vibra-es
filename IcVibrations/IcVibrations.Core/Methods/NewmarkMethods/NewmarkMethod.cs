﻿using IcVibrations.Calculator.MainMatrixes;
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
        private double a0, a1, a2, a3, a4, a5;
        // Boundary condition true
        private int bcTrue;
        // Angular frequency 
        private double wf, wi, dw, w;
        // Time
        private double dt, t0;
        private int pD, pC;

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
            int angularFrequencyLoopCount = (int)((wf - wi) / dw) + 1;

            int resultSize = angularFrequencyLoopCount * pC * pD;

            NewmarkMethodOutput output = new NewmarkMethodOutput
            {
                Result = new double[resultSize, bcTrue],
                Time = new double[resultSize]
            };

            for (int i = 0; i < angularFrequencyLoopCount; i++)
            {
                w = wi + (i * dw);
                //dt = 0.1;
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

                this.Solution(input, output, response);
            }

            return output;
        }

        public NewmarkMethodInput CreateInput(BeamRequestData requestData, Beam beam, int degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum, beam.ElementCount);

            double[,] hardness = this._mainMatrix.CalculateHardness(beam, degreesFreedomMaximum, beam.ElementCount);

            double[,] damping = this._mainMatrix.CalculateDamping(mass, hardness, degreesFreedomMaximum);

            double[] forces = beam.Forces;

            bool[] bondaryCondition = this._mainMatrix.CalculateBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            bcTrue = 0;
            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    bcTrue += 1;
                }
            }

            // Output values
            input.Mass = this._auxiliarMethod.AplyBondaryConditions(mass, bondaryCondition, bcTrue);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardness, bondaryCondition, bcTrue);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(damping, bondaryCondition, bcTrue);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(forces, bondaryCondition, bcTrue);

            t0 = requestData.InitialTime;

            pD = requestData.PeriodDivion;
            
            pC = requestData.PeriodCount;
            
            wi = requestData.InitialAngularFrequency;
            
            dw = requestData.DeltaAngularFrequency;
            
            wf = requestData.FinalAngularFrequency;

            return input;
        }

        private void Solution(NewmarkMethodInput input, NewmarkMethodOutput output, OperationResponseBase response)
        {
            //StreamWriter streamWriter = new StreamWriter(@"C:\Users\bruno\OneDrive\Documentos\GitHub\IC_Vibra-es\IcVibrations\RectangularBeamSolution.csv");

            int i, jn, jp;
            
            double time = t0;

            int count = 0;

            double[] force = new double[bcTrue];
            for (i = 0; i < bcTrue; i++)
            {
                force[i] = input.Force[i];
            }

            double[] y = new double[bcTrue];
            double[] yAnt = new double[bcTrue];
            double[] deltaY = new double[bcTrue];

            double[] vel = new double[bcTrue];
            double[] velAnt = new double[bcTrue];
            double[] deltaVel = new double[bcTrue];

            double[] acel = new double[bcTrue];
            double[] acelAnt = new double[bcTrue];
            double[] deltaAcel = new double[bcTrue];

            double[] forceAnt = new double[bcTrue];

            for (jp = 0; jp < pC; jp++)
            {
                for (jn = 0; jn < pD; jn++)            
                {
                    //for (i = 0; i < bcTrue; i++)
                    //{
                    //    // Force can't initiate with 0
                    //    input.Force[i] = force[i] * Math.Cos(w * time);
                    //}

                    if (time == 0)
                    {
                        double[,] massInverse = this._arrayOperation.InverseMatrix(input.Mass);

                        double[] matrix_K_C_Y = this.CreateMatrix_K_C_Y(input, y, response);

                        acel = this._arrayOperation.Multiply(massInverse, matrix_K_C_Y);

                        for (i = 0; i < bcTrue; i++)
                        {
                            acelAnt[i] = acel[i];                
                        }
                    }

                    if (jp >= 0)
                    {
                        if (w == 0)
                        {
                            for(int k = 0; k < bcTrue; k++)
                            {
                                output.Result[count, k] = y[k];
                                output.Time[count] = time;
                            }

                            count += 1;
                            
                            //try
                            //{
                                //using (StreamWriter sw = streamWriter)
                                //{
                                    //sw.WriteLine(string.Format("{0},{1},{2},{3}", w, time, y, vel, acel, input.Force));

                                    //sw.Close();
                                //}
                            //}
                            //catch
                            //{
                                //// Não quero que pare, só avise que deu erro.
                                //throw new Exception("Couldn't open file.");
                            //}
                        }
                    }

                    double[,] equivalentHardness = this.CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness);
                    double[] equivalentForce = this.CalculateEquivalentForce(input, forceAnt, vel, acel);

                    double[,] equivalentHardnessInverse = this._arrayOperation.InverseMatrix(equivalentHardness);

                    deltaY = this._arrayOperation.Multiply(equivalentForce,equivalentHardnessInverse);

                    for (i = 0; i < bcTrue; i++)
                    {
                        deltaVel[i] = a1 * deltaY[i] - a3 * velAnt[i] - a5 * acelAnt[i];
                    }

                    for (i = 0; i < bcTrue; i++)
                    {
                        deltaAcel[i] = a0 * deltaY[i] - a2 * velAnt[i] - a4 * acelAnt[i];
                    }

                    for (i = 0; i < bcTrue; i++)
                    {
                        y[i] = yAnt[i] + deltaY[i];
                        vel[i] = velAnt[i] + deltaVel[i];
                        acel[i] = acelAnt[i] + deltaAcel[i];
                    }

                    time += dt;

                    for (i = 0; i < bcTrue; i++)
                    {
                        yAnt[i] = y[i];
                        velAnt[i] = vel[i];
                        acelAnt[i] = acel[i];
                    }

                    for (i = 0; i < bcTrue; i++)
                    {
                        forceAnt[i] = input.Force[i];
                    }
                }
            }
        }

        private double[,] CalculateEquivalentHardness(double[,] mass, double[,] damping, double[,] hardness)
        {
            double[,] equivalentHardness = new double[bcTrue, bcTrue];

            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    equivalentHardness[i,j] = a0 * mass[i,j] + a1 * damping[i,j] + hardness[i,j];
                }
            }

            return equivalentHardness;
        }

        private double[,] CalculateMatrixP1(double[,] mass, double[,] damping)
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

        private double[,] CalculateMatrixP2(double[,] mass, double[,] damping)
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

        private double[] CalculateEquivalentForce(NewmarkMethodInput input, double[] force_ant, double[] vel, double[] acel)
        {
            double[] deltaForce = this._arrayOperation.Subtract(input.Force, force_ant);

            double[,] p1 = this.CalculateMatrixP1(input.Mass, input.Damping);

            double[,] p2 = this.CalculateMatrixP2(input.Mass, input.Damping);

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
