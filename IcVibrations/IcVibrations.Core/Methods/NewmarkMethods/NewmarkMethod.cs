using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Methods.AuxiliarOperations;
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

        public NewmarkMethodOutput CreateOutput(NewmarkMethodInput input, OperationResponseBase response)
        {
            int angularFrequencyLoopCount;
            if (dw != 0)
            {
                angularFrequencyLoopCount = (int)((wf - wi) / dw) + 1;
            }
            else
            {
                angularFrequencyLoopCount = 1;

            }

            NewmarkMethodOutput output = new NewmarkMethodOutput
            {
                IterationsResult = new List<IterationResult>()
            };

            // More iterations make the application slower
            for (int i = 0; i < angularFrequencyLoopCount; i++)
            {
                w = wi + (i * dw);

                output.AngularFrequency = w;

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

                try
                {
                    this.Solution(input, output, response);
                }
                catch (Exception ex)
                {
                    response.AddError("000", $"Error executing the solution. {ex.Message}");

                    return null;
                }
            }

            return output;
        }

        public NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, RectangularBeam beam, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            uint piezoelectricDegreesFreedomMaximum = beam.ElementCount + 1;

            // Calculate geometric properties
            double beamArea = this._geometricProperty.Area(beam.Height, beam.Width, beam.Thickness);

            double beamMomentInertia = this._geometricProperty.MomentInertia(beam.Height, beam.Width, beam.Thickness);

            double piezoelectricArea = this._geometricProperty.Area(piezoelectric.Height, piezoelectric.Width, piezoelectric.Thickness);

            double piezoelectricMomentInertia = this._geometricProperty.MomentInertia(piezoelectric.Height, piezoelectric.Width, piezoelectric.Thickness);

            // Create geometric properties matrixes
            beam.GeometricProperty.Area = this._arrayOperation.Create(beamArea, beam.ElementCount);

            beam.GeometricProperty.MomentOfInertia = this._arrayOperation.Create(beamMomentInertia, beam.ElementCount);

            piezoelectric.GeometricProperty.Area = this._arrayOperation.Create(piezoelectricArea, beam.ElementCount, piezoelectric.ElementsWithPiezoelectric, $"{nameof(piezoelectric)} {nameof(piezoelectric.GeometricProperty.Area)}");

            piezoelectric.GeometricProperty.MomentOfInertia = this._arrayOperation.Create(piezoelectricMomentInertia, beam.ElementCount, piezoelectric.ElementsWithPiezoelectric, $"{nameof(piezoelectric)} {nameof(piezoelectric.GeometricProperty.MomentOfInertia)}");

            // Calculate basic matrix
            double[,] mass = this._mainMatrix.CalculateMass(beam, piezoelectric, degreesFreedomMaximum);

            double[,] hardness = this._mainMatrix.CalculateHardness(beam, piezoelectric, degreesFreedomMaximum);

            double[,] piezoelectricElectromechanicalCoupling = this._mainMatrix.CalculatePiezoelectricElectromechanicalCoupling(beam.Height, beam.ElementCount, piezoelectric, degreesFreedomMaximum);

            double[,] piezoelectricCapacitance = this._mainMatrix.CalculatePiezoelectricCapacitance(piezoelectric, beam.ElementCount);

            // Calculate input
            input.Mass = this._mainMatrix.CalculateEquivalentMass(mass, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);

            input.Hardness = this._mainMatrix.CalculateEquivalentHardness(hardness, piezoelectricElectromechanicalCoupling, piezoelectricCapacitance, degreesFreedomMaximum, piezoelectricDegreesFreedomMaximum);

            input.Damping = this._mainMatrix.CalculateDamping(input.Mass, input.Hardness, degreesFreedomMaximum);

            input.Force = beam.Forces;

            return input;
        }

        public NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, BeamWithDva beam, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = this._mainMatrix.CalculateBeamHardness(beam, degreesFreedomMaximum);

            double[,] massWithDva = this._mainMatrix.CalculateMassWithDva(mass, beam.DvaMasses, beam.DvaNodePositions);

            double[,] hardnessWithDva = this._mainMatrix.CalculateBeamHardnessWithDva(hardness, beam.DvaHardnesses, beam.DvaNodePositions);

            double[,] dampingWithDva = this._mainMatrix.CalculateDamping(massWithDva, hardnessWithDva, degreesFreedomMaximum);

            double[] forces = beam.Forces;

            bool[] bondaryCondition = this._mainMatrix.CalculateBeamBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

            bcTrue = 0;
            for (int i = 0; i < degreesFreedomMaximum; i++)
            {
                if (bondaryCondition[i] == true)
                {
                    bcTrue += 1;
                }
            }

            // Output values
            input.Mass = this._auxiliarMethod.AplyBondaryConditions(massWithDva, bondaryCondition, bcTrue);

            input.Hardness = this._auxiliarMethod.AplyBondaryConditions(hardnessWithDva, bondaryCondition, bcTrue);

            input.Damping = this._auxiliarMethod.AplyBondaryConditions(dampingWithDva, bondaryCondition, bcTrue);

            input.Force = this._auxiliarMethod.AplyBondaryConditions(forces, bondaryCondition, bcTrue);

            t0 = newmarkMethodParameter.InitialTime;

            pD = newmarkMethodParameter.PeriodDivion;

            pC = newmarkMethodParameter.PeriodCount;

            wi = newmarkMethodParameter.InitialAngularFrequency;

            dw = newmarkMethodParameter.DeltaAngularFrequency.Value;

            wf = newmarkMethodParameter.FinalAngularFrequency;

            return input;
        }

        public NewmarkMethodInput CreateInput(NewmarkMethodParameter newmarkMethodParameter, Beam beam, uint degreesFreedomMaximum)
        {
            NewmarkMethodInput input = new NewmarkMethodInput();

            // Calculate values
            double[,] mass = this._mainMatrix.CalculateMass(beam, degreesFreedomMaximum);

            double[,] hardness = this._mainMatrix.CalculateBeamHardness(beam, degreesFreedomMaximum);

            double[,] damping = this._mainMatrix.CalculateDamping(mass, hardness, degreesFreedomMaximum);

            double[] forces = beam.Forces;

            bool[] bondaryCondition = this._mainMatrix.CalculateBeamBondaryCondition(beam.FirstFastening, beam.LastFastening, degreesFreedomMaximum);

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

            t0 = newmarkMethodParameter.InitialTime;

            pD = newmarkMethodParameter.PeriodDivion;

            pC = newmarkMethodParameter.PeriodCount;

            wi = newmarkMethodParameter.InitialAngularFrequency;

            dw = newmarkMethodParameter.DeltaAngularFrequency.Value;

            wf = newmarkMethodParameter.FinalAngularFrequency;

            return input;
        }

        public List<IterationResult> Solution(NewmarkMethodInput input, NewmarkMethodOutput output, OperationResponseBase response)
        {
            List<IterationResult> results = new List<IterationResult>();

            int i, jn, jp;
            double time = t0;

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
                    for (i = 0; i < bcTrue; i++)
                    {
                        // Force can't initiate in 0?
                        input.Force[i] = force[i] * Math.Cos(w * time);
                    }

                    if (time == 0)
                    {
                        double[,] massInverse = this._arrayOperation.InverseMatrix(input.Mass, bcTrue, nameof(massInverse));

                        double[] matrix_K_Y = this._arrayOperation.Multiply(input.Hardness, y, nameof(matrix_K_Y));

                        double[] matrix_C_Vel = this._arrayOperation.Multiply(input.Damping, vel, nameof(matrix_C_Vel));

                        double[] subtractionResult = this._arrayOperation.Subtract(input.Force, matrix_K_Y, matrix_C_Vel, $"{nameof(input.Force)}, {nameof(matrix_K_Y)}, {nameof(matrix_C_Vel)}");

                        acel = this._arrayOperation.Multiply(massInverse, subtractionResult, $"{nameof(massInverse)}, {nameof(subtractionResult)}");

                        for (i = 0; i < bcTrue; i++)
                        {
                            acelAnt[i] = acel[i];
                        }
                    }

                    if (jp >= 0)
                    {
                        IterationResult iterationResult = new IterationResult
                        {
                            Time = time,
                            Displacemens = y,
                            Velocities = vel,
                            Acelerations = acel,
                            Forces = input.Force
                        };

                        results.Add(iterationResult);

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

                    double[,] equivalentHardness = this.CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness);
                    double[] equivalentForce = this.CalculateEquivalentForce(input, forceAnt, vel, acel);

                    double[,] inversedEquivalentHardness = this._arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness));

                    deltaY = this._arrayOperation.Multiply(equivalentForce, inversedEquivalentHardness, $"{nameof(equivalentForce)}, {nameof(inversedEquivalentHardness)}");

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

            return results;
        }

        public double[,] CalculateEquivalentHardness(double[,] mass, double[,] damping, double[,] hardness)
        {
            double[,] equivalentHardness = new double[bcTrue, bcTrue];

            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    equivalentHardness[i, j] = a0 * mass[i, j] + a1 * damping[i, j] + hardness[i, j];
                }
            }

            return equivalentHardness;
        }

        public double[,] CalculateMatrixP1(double[,] mass, double[,] damping)
        {
            double[,] p1 = new double[bcTrue, bcTrue];

            for (int i = 0; i < bcTrue; i++)
            {
                for (int j = 0; j < bcTrue; j++)
                {
                    p1[i, j] = a2 * mass[i, j] + a3 * damping[i, j];
                }
            }

            return p1;
        }

        public double[,] CalculateMatrixP2(double[,] mass, double[,] damping)
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

        public double[] CalculateEquivalentForce(NewmarkMethodInput input, double[] force_ant, double[] vel, double[] acel)
        {
            double[] deltaForce = this._arrayOperation.Subtract(input.Force, force_ant, $"{nameof(input.Force)}, {nameof(force_ant)}");

            double[,] p1 = this.CalculateMatrixP1(input.Mass, input.Damping);

            double[,] p2 = this.CalculateMatrixP2(input.Mass, input.Damping);

            double[] vel_p1 = this._arrayOperation.Multiply(p1, vel, $"{nameof(p1)}, {nameof(vel)}");

            double[] acel_p2 = this._arrayOperation.Multiply(p2, acel, $"{nameof(p2)}, {nameof(acel)}");

            double[] equivalentForce = _arrayOperation.Sum(deltaForce, vel_p1, acel_p2, $"{nameof(deltaForce)}, {nameof(vel_p1)}, {nameof(acel_p2)}");

            return equivalentForce;
        }
    }
}
