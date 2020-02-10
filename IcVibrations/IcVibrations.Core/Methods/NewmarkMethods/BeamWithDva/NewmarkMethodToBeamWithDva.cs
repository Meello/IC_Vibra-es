using IcVibrations.Calculator.GeometricProperties;
using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.Methods.NewmarkMethods.BeamWithDva
{
    public abstract class NewmarkMethodToBeamWithDva<TProfile> : NewmarkMethod<BeamWithDva<TProfile>, TProfile>
        where TProfile : Profile, new()
    {
        private readonly IMainMatrix _mainMatrix;
        private readonly IAuxiliarOperation _auxiliarMethod;
        private readonly IArrayOperation _arrayOperation;
        private readonly ICalculateGeometricProperty _geometricProperty;

        public NewmarkMethodToBeamWithDva(
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

        public override Task<NewmarkMethodInput> CreateInput(NewmarkMethodParameter newmarkMethodParameter, BeamWithDva<TProfile> beam)
        {
            throw new NotImplementedException();
        }
    }
}
