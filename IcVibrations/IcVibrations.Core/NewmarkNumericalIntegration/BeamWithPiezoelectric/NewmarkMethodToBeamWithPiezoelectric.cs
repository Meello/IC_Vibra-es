using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Methods.AuxiliarOperations;
using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.NewmarkNumericalIntegration.BeamWithPiezoelectric
{
    public abstract class NewmarkMethodToBeamWithPiezoelectric<TProfile> : NewmarkMethod<BeamWithPiezoelectric<TProfile>, TProfile>
        where TProfile : Profile, new()
    {
        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarOperation _auxiliarMethod;
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _geometricProperty;

        public NewmarkMethodToBeamWithPiezoelectric(
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

        public override Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, BeamWithPiezoelectric<TProfile> beam)
        {
            throw new NotImplementedException();
        }
    }
}

