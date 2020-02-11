using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.DataContracts.CalculateVibration;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateVibration<TRequest, TRequestData, TProfile, TBeam> : OperationBase<TRequest, CalculateVibrationResponse>, ICalculateVibration<TRequest, TRequestData, TProfile, TBeam>
        where TProfile : Profile, new()
        where TRequestData : CalculateVibrationRequestData<TProfile>, new()
        where TRequest : CalculateVibrationRequest<TProfile, TRequestData>
        where TBeam : IBeam<TProfile>, new()
    {
        private readonly INewmarkMethod<TBeam, TProfile> _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;
        private readonly IProfileValidator<TProfile> _profileValidator;
        private readonly IAuxiliarOperation _auxiliarOperation;

        public CalculateVibration(
            INewmarkMethod<TBeam, TProfile> newmarkMethod,
            IMappingResolver mappingResolver,
            IProfileValidator<TProfile> profileValidator,
            IAuxiliarOperation auxiliarOperation)
        {
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
            this._profileValidator = profileValidator;
            this._auxiliarOperation = auxiliarOperation;
        }

        /// <summary>
        /// It's responsible to build the beam.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract Task<TBeam> BuildBeam(TRequest request, uint degreesFreedomMaximum);

        protected override async Task<CalculateVibrationResponse> ProcessOperation(TRequest request)
        {
            CalculateVibrationResponse response = new CalculateVibrationResponse();

            uint degreesFreedomMaximum = this._auxiliarOperation.CalculateDegreesFreedomMaximum(request.BeamData.NumberOfElements);

            TBeam beam = await this.BuildBeam(request, degreesFreedomMaximum);

            NewmarkMethodInput input = await this._newmarkMethod.CreateInput(request.MethodParameterData, beam);

            NewmarkMethodOutput output = await this._newmarkMethod.CreateOutput(input, response);

            response.Data = this._mappingResolver.BuildFrom(output, request.Author, request.AnalysisExplanation);

            return response;
        }

        /// <summary>
        /// It's responsible to validade
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateVibrationResponse> ValidateOperation(TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
