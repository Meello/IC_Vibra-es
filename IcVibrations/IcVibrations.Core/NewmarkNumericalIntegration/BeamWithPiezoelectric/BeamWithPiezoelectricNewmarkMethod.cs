using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Methods.AuxiliarOperations;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to do the newmark numerical integration to the condition of a beam with piezoelectric.
    /// </summary>
    public class BeamWithPiezoelectricNewmarkMethod : NewmarkMethod<NewmarkMethodBeamWithPiezoelectricInput>, IBeamWithPiezoelectricNewmarkMethod
    {
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="auxiliarOperation"></param>
        public BeamWithPiezoelectricNewmarkMethod(
            IArrayOperation arrayOperation,
            IAuxiliarOperation auxiliarOperation)
            : base(arrayOperation, auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
        }

        public override async Task<double[]> CalculateAcelInTime0(NewmarkMethodBeamWithPiezoelectricInput input)
        {
            double[] beamAcel = new double[input.NumberOfTrueBeamBoundaryConditions];
            double[] acel = new double[input.NumberOfTrueBoundaryConditions];

            double[,] mass = new double[input.NumberOfTrueBeamBoundaryConditions, input.NumberOfTrueBeamBoundaryConditions];
            double[,] hardness = new double[input.NumberOfTrueBeamBoundaryConditions, input.NumberOfTrueBeamBoundaryConditions];
            double[,] damping = new double[input.NumberOfTrueBeamBoundaryConditions, input.NumberOfTrueBeamBoundaryConditions];
            double[] force = new double[input.NumberOfTrueBeamBoundaryConditions];
            double[] displacement = new double[input.NumberOfTrueBeamBoundaryConditions];
            double[] velocity = new double[input.NumberOfTrueBeamBoundaryConditions];


            for (int i = 0; i < input.NumberOfTrueBeamBoundaryConditions; i++)
            {
                for (int j = 0; j < input.NumberOfTrueBeamBoundaryConditions; j++)
                {
                    mass[i, j] = input.Mass[i, j];
                    hardness[i, j] = input.Hardness[i, j];
                    damping[i, j] = input.Damping[i, j];
                }

                force[i] = input.Force[i];
            }

            double[,] massInverse;
            double[] matrix_K_Y;
            double[] matrix_C_Vel;

            var massInverseTask = Task.Run(async () =>
            {
                return await this._arrayOperation.InverseMatrix(mass, nameof(massInverse)).ConfigureAwait(false);
            });

            var matrix_K_YTask = Task.Run(async () =>
            {
                return await this._arrayOperation.Multiply(hardness, displacement, nameof(matrix_K_Y)).ConfigureAwait(false);
            });

            var matrix_C_VelTask = Task.Run(async () =>
            {
                return await this._arrayOperation.Multiply(damping, velocity, nameof(matrix_C_Vel)).ConfigureAwait(false);
            });

            await Task.WhenAll(massInverseTask, matrix_K_YTask, matrix_C_VelTask).ConfigureAwait(false);

            massInverse = massInverseTask.Result;
            matrix_K_Y = matrix_K_YTask.Result;
            matrix_C_Vel = matrix_C_VelTask.Result;

            double[] subtractionResult = await this._arrayOperation.Subtract(force, matrix_K_Y, matrix_C_Vel, $"{nameof(force)}, {nameof(matrix_K_Y)}, {nameof(matrix_C_Vel)}");

            beamAcel = await this._arrayOperation.Multiply(massInverse, subtractionResult, $"{nameof(massInverse)}, {nameof(subtractionResult)}");

            for (int i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                if (i < input.NumberOfTrueBeamBoundaryConditions)
                {
                    acel[i] = beamAcel[i];
                }
                else
                {
                    acel[i] = 0;
                }
            }

            return acel;
        }
    }
}
