using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO;
using IcVibrations.Core.DTO.Input;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.CalculateVibration;
using IcVibrations.Methods.AuxiliarOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TProfile"></typeparam>
    /// <typeparam name="TBeam"></typeparam>
    public abstract class CalculateVibration<TRequest, TRequestData, TProfile, TBeam, TInput> : OperationBase<TRequest, CalculateVibrationResponse>, ICalculateVibration<TRequest, TRequestData, TProfile, TBeam, TInput>
        where TProfile : Profile, new()
        where TRequestData : CalculateVibrationRequestData<TProfile>, new()
        where TRequest : CalculateVibrationRequest<TProfile, TRequestData>
        where TBeam : AbstractBeam<TProfile>, new()
        where TInput : INewmarkMethodInput
    {
        private readonly INewmarkMethod<TInput> _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;
        private readonly IProfileValidator<TProfile> _profileValidator;
        private readonly IAuxiliarOperation _auxiliarOperation;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateVibration(
            INewmarkMethod<TInput> newmarkMethod,
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

        /// <summary>
        /// It's responsible to calculate the input to newmark integration method.
        /// </summary>
        /// <param name="beam"></param>
        /// <param name="newmarkMethodParameter"></param>
        /// <returns></returns>
        public abstract Task<TInput> CreateInput(TBeam beam, NewmarkMethodParameter newmarkMethodParameter, uint degreesFreedomMaximum);

        protected override async Task<CalculateVibrationResponse> ProcessOperation(TRequest request)
        {
            CalculateVibrationResponse response = new CalculateVibrationResponse();
            
            try
            {
                uint degreesFreedomMaximum = this._auxiliarOperation.CalculateDegreesFreedomMaximum(request.BeamData.NumberOfElements);

                TBeam beam = await this.BuildBeam(request, degreesFreedomMaximum);

                TInput input = await this.CreateInput(beam, request.MethodParameterData, degreesFreedomMaximum);

                IAsyncEnumerable<Analysis> output = this._newmarkMethod.CalculateResponse(input, response);

                //response.Data = this._mappingResolver.BuildFrom(output, request.Author, request.AnalysisExplanation);

                return response;
            }
            catch(Exception ex)
            {
                response.AddError("000", ex.Message);

                return response;
            }
        }

        /// <summary>
        /// It's responsible to validade
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async override Task<CalculateVibrationResponse> ValidateOperation(TRequest request)
        {
            CalculateVibrationResponse response = new CalculateVibrationResponse();

            if(!await this._profileValidator.Execute(request.BeamData.Profile))
            {
                return response;
            }

            return response;
        }
    }
}
