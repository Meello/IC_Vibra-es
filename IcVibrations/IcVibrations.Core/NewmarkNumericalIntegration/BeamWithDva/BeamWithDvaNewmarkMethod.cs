using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Methods.AuxiliarOperations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorbers.
    /// </summary>
    public class BeamWithDvaNewmarkMethod : NewmarkMethod, IBeamWithDvaNewmarkMethod
    {
        private readonly IArrayOperation _arrayOperation;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="arrayOperation"></param>
        /// <param name="auxiliarOperation"></param>
        public BeamWithDvaNewmarkMethod(
            IArrayOperation arrayOperation, 
            IAuxiliarOperation auxiliarOperation) 
            : base(arrayOperation, auxiliarOperation)
        {
            this._arrayOperation = arrayOperation;
        }

        protected override async Task<double[]> CalculateDisplacement(NewmarkMethodInput input, double[] previousDisplacement, double[] previousVelocity, double[] previousAcceleration)
        {
            double[,] equivalentHardness = await base.CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness, input.NumberOfTrueBoundaryConditions).ConfigureAwait(false);
            double[,] inversedEquivalentHardness = await this._arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness)).ConfigureAwait(false);

            double[] equivalentForce = await this.CalculateEquivalentForce(input, previousDisplacement, previousVelocity, previousAcceleration, input.NumberOfTrueBoundaryConditions).ConfigureAwait(false);

            uint size = input.NumberOfTrueBoundaryConditions - input.NumberOfDvas;

            double[] dvaForce = new double[size];
            double[,] dvaInversedHardness = new double[size, size];

            for(uint i = 0; i < size; i++)
            {
                dvaForce[i] = equivalentForce[i];

                for(uint j = 0; j < size; j++)
                {
                    dvaInversedHardness[i, j] = inversedEquivalentHardness[i, j];
                }
            }

            double[] displacement = await this._arrayOperation.Multiply(dvaForce, dvaInversedHardness, $"{nameof(dvaForce)}, {nameof(dvaInversedHardness)}");

            return displacement;
        }
    }
}
