using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using System.Threading.Tasks;

namespace IcVibrations.Core.Methods.NewmarkMethods.Beam
{
    public class NewmarkMethodToBeam : NewmarkMethod<Models.Beam.Beam>
    {
        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarOperation _auxiliarMethod;
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _geometricProperty;

        public NewmarkMethodToBeam(
            IMainMatrix mainMatrix,
            IAuxiliarOperation auxiliarMethod,
            IArrayOperation arrayOperation, 
            ICalculateGeometricProperty geometricProperty) 
            : base(mainMatrix, auxiliarMethod, arrayOperation, geometricProperty)
        {
            this._mainMatrix = mainMatrix;
            this._auxiliarMethod = auxiliarMethod;
            this._arrayOperation = arrayOperation;
            this._geometricProperty = geometricProperty;
        }

        public override Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, Models.Beam.Beam beam)
        {
            return base.CreateInput(newmarkMethodParameter, beam);
        }
    }
}
