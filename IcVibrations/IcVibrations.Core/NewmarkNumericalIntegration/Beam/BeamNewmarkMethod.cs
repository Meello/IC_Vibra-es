using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Methods.AuxiliarOperations;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration.Beam
{
    /// <summary>
    /// It's responsible to do the newmark numerical integration to the condition of a beam.
    /// </summary>
    public class BeamNewmarkMethod : NewmarkMethod<NewmarkMethodBeamInput>, IBeamNewmarkMethod
    {
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="auxiliarOperation"></param>
        public BeamNewmarkMethod(
            IArrayOperation arrayOperation, 
            IAuxiliarOperation auxiliarOperation) 
            : base(arrayOperation, auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
        }

        public override async Task<double[]> CalculateAcelInTime0(NewmarkMethodBeamInput input)
        {
            double[] displacement = new double[input.NumberOfTrueBoundaryConditions];
            double[] velocity = new double[input.NumberOfTrueBoundaryConditions];
            double[] acel = new double[input.NumberOfTrueBoundaryConditions];

            double[,] massInverse;
            double[] matrix_K_Y;
            double[] matrix_C_Vel;

            var massInverseTask = Task.Run(async () =>
            {
                return await _arrayOperation.InverseMatrix(input.Mass, input.NumberOfTrueBoundaryConditions, nameof(massInverse)).ConfigureAwait(false);
            });

            var matrix_K_YTask = Task.Run(async () =>
            {
                return await _arrayOperation.Multiply(input.Hardness, displacement, nameof(matrix_K_Y)).ConfigureAwait(false);
            });

            var matrix_C_VelTask = Task.Run(async () =>
            {
                return await _arrayOperation.Multiply(input.Damping, velocity, nameof(matrix_C_Vel)).ConfigureAwait(false);
            });

            await Task.WhenAll(massInverseTask, matrix_K_YTask, matrix_C_VelTask).ConfigureAwait(false);

            massInverse = massInverseTask.Result;
            matrix_K_Y = matrix_K_YTask.Result;
            matrix_C_Vel = matrix_C_VelTask.Result;

            double[] subtractionResult = await this._arrayOperation.Subtract(input.Force, matrix_K_Y, matrix_C_Vel, $"{nameof(input.Force)}, {nameof(matrix_K_Y)}, {nameof(matrix_C_Vel)}");

            acel = await this._arrayOperation.Multiply(massInverse, subtractionResult, $"{nameof(massInverse)}, {nameof(subtractionResult)}");

            return acel;
        }
    }
}
