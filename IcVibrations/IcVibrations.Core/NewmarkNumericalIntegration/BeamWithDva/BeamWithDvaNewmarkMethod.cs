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
            double[,] equivalentHardness = await base.CalculateEquivalentHardness(input.Mass, input.Damping, input.Hardness, input.NumberOfTrueBoundaryConditions + input.NumberOfDvas).ConfigureAwait(false);
            double[,] inversedEquivalentHardness = await this._arrayOperation.InverseMatrix(equivalentHardness, nameof(equivalentHardness)).ConfigureAwait(false);

            double[] equivalentForce = await this.CalculateEquivalentForce(input, previousDisplacement, previousVelocity, previousAcceleration, input.NumberOfTrueBoundaryConditions).ConfigureAwait(false);

            double[,] dvaInversedHardness = new double[input.NumberOfTrueBoundaryConditions, input.NumberOfTrueBoundaryConditions];

            for(uint i = 0; i < input.NumberOfTrueBoundaryConditions; i++)
            {
                for(uint j = 0; j < input.NumberOfTrueBoundaryConditions; j++)
                {
                    dvaInversedHardness[i, j] = inversedEquivalentHardness[i, j];
                }
            }

            double[] displacement = await this._arrayOperation.Multiply(equivalentForce, dvaInversedHardness, $"{nameof(equivalentForce)}, {nameof(dvaInversedHardness)}");

            return displacement;
        }
    }
}
