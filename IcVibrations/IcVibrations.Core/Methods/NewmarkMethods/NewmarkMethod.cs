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
        private double glMax;
        private int bcTrue;

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

            int nbif = (int)((input.FinalAngularFrequency - input.InitialAngularFrequency) / input.TimeDivion) + 1;

            for (int i = 0; i < nbif; i++)
            {
                //W = Wi + (jb * dW);
                //printf("W = %g\n", W);
                //if (W != 0) dt = (long double) ((PI * 2./ W) / nm);
                //else dt = (long double) ((PI * 2.) / nm);

                //MatrizP1();
                //solucao();
            }

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

            input.InitialTime = requestData.InitialTime;

            input.TimeDivion = requestData.TimeDivion;
            
            input.FinalTime = requestData.FinalTime;
            
            input.InitialAngularFrequency = requestData.InitialAngularFrequency;
            
            input.AngularFrequencyDivision = requestData.AngularFrequencyDivision;
            
            input.FinalAngularFrequency = requestData.FinalAngularFrequency;

            return input;
        }

        private NewmarkMethodOutput Solution(NewmarkMethodInput input)
        {
            NewmarkMethodOutput output = new NewmarkMethodOutput();

            double z;
            int i, jn, jp;

            //cont1 = 0;
            //for (jp = 1; jp <= np - 1; jp++)
            //{
                //for (jn = 1; jn <= nm - 1; jn++)            /* time loop */
                //{
                    //f[0] = f1[0] * sin(W * t);
                    //f[1] = f1[1] * sin(W * t);
                    //f[2] = f1[2] * sin(W * t);
                    //f[3] = f1[3] * sin(W * t);
                    //f[4] = f1[4] * sin(W * t);
                    //f[5] = f1[5] * sin(W * t);
                    //CondicoesContorno1();
                    //if (t == 0)
                    //{
                        //for (i = 0; i < 3 * (N - cont); i++)
                        //{
                            //y[i] = y_ant[i] = 0.0;              /* initial position */
                        //}

                        //Matriz_K_Y();
                        //Matriz_C_Y();
                        //Matriz_K_C_Y();
                        //Elim_Gauss1();

                        //for (i = 0; i < (N - cont); i++)
                        //{
                            //y[i + 2 * (N - cont)] = acel[i];                /* initial position */
                        //}

                        //for (i = 2 * (N - cont); i < 3 * (N - cont); i++)
                        //{
                            //y_ant[i] = y[i];                /* initial position */
                        //}

                    //}

                    //if (jp > 0)
                    //{
                        //if (sqrt((W - 0.5) * (W - 0.5)) < 0.0001)
                        //{
                            //fprintf(output, "%Lf\t %Lf\t %Lf\t %Lf\t %Lf\t %Lf\n", W, t, y[0], y[1], y[2], y[3]);
                        //}
                    //}

                    //MatrizP1();
                    //MatrizP2();
                    //MatrizP3();
                    //MatrizP4();
                    //Elim_Gauss();

                    //for (i = 0; i < (N - cont); i++)
                    //{
                        //delta_y[i] = P7[i];
                    //}


                    //for (i = 0; i < (N - cont); i++)
                    //{
                        //delta_y[i + 2 * (N - cont)] = a1 * (P7[i]) - a3 * y_ant[i + (N - cont)] - a5 * y_ant[i + 2 * (N - cont)];
                    //}

                    //for (i = 0; i < (N - cont); i++)
                    //{
                        //delta_y[i + (N - cont)] = a0 * (P7[i]) - a2 * y_ant[i + (N - cont)] - a4 * y_ant[i + 2 * (N - cont)];
                    //}

                    //for (i = 0; i <= 3 * (N - cont); i++)
                    //{
                        //y[i] = y_ant[i] + delta_y[i];
                    //}

                    //t = t + dt;

                    //for (i = 0; i < 3 * (N - cont); i++)
                    //{
                        //y_ant[i] = y[i];         /* initial position */
                    //}

                    //for (i = 0; i < (N - cont); i++)
                    //{
                        //fa_ant[i] = fa[i];       /* initial position */
                    //}

                //}
            //}

            return output;
        }

        private double[,] MatrizP1(double[,] mass, double[,] damping, double[,] hardness)
        {
            double[,] P1 = new double[bcTrue, bcTrue];

            /* montagem da matriz massa*/
            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    P1[i,j] = a0 * mass[i,j] + a1 * damping[i,j] + hardness[i,j];
                }
            }

            return P1;
        }

        private double[,] MatrizP2a(void)
        {
            int i, j;
            double soma, soma1;

            for (i = 0; i < N - cont; i++)
            {

                for (j = 0; j < N - cont; j++)
                {
                    P2a[i,j] = a2 * M_cc[j,i] + a3 * C[i,j];

                }
            }

        }
        
        void MatrizP2(void)
        {
            int i, j;
            long double soma, soma1;

            for (i = 0; i < N - cont; i++)
            {
                soma = 0.;
                for (j = 0; j < N - cont; j++)
                {
                    soma1 = P2a[i,j] * y_ant[j + N - cont];
                }
                P2[j] = soma;
            }

        }

        /***********************************************************************/
        /***************************************************/
        void MatrizP3a(void)
        {
            int i, j;
            long double soma, soma1;

            for (i = 0; i < N - cont; i++)
            {

                for (j = 0; j < N - cont; j++)
                {
                    P3a[i,j] = a4 * M_cc[j,i] + a5 * C[i,j];

                }

            }

        }

        /***********************************************************************/
        void MatrizP3(void)
        {
            int i, j;
            long double soma;

            for (j = 0; j < N - cont; j++)
            {
                soma = 0.;
                for (i = 0; i < N - cont; i++)
                {
                    soma = soma + P3a[j,i] * y_ant[i + 2 * (N - cont)];
                }
                P3[j] = soma;
            }

        }


        /***********************************************************************/
        void MatrizP4(void)
        {
            /* montagem da matriz massa*/
            for (i = 0; i < N - cont; i++)
            {
                P6[i] = fa[i] - fa_ant[i];
                P4[i] = P6[i] + P2[i] + P3[i];

            }
        }

        /***********************************************************************/
        void Matriz_K_Y(void)
        {
            int i, j;
            long double soma;

            for (j = 0; j < N - cont; j++)
            {
                soma = 0.;
                for (i = 0; i < N - cont; i++)
                {
                    soma = soma + (K_cc[j,i] * y[i]);
                }
                K_Y[j] = soma;
            }

        }

        /*****************************************************/
        void Matriz_C_Y(void)
        {
            int i, j;
            long double soma;

            for (j = 0; j < N - cont; j++)
            {
                soma = 0.;
                for (i = 0; i < N - cont; i++)
                {
                    soma = soma + (C[j,i] * y[i + (N - cont)]);
                }
                C_Y[j] = soma;
            }

        }
        /***********************************************************************/
        void Matriz_K_C_Y(void)
        {
            /* montagem da matriz massa*/
            for (i = 0; i < N - cont; i++)
            {

                K_M_Y[i] = fa[i] - C_Y[i] - K_Y[i];

            }
        }
    }
}
