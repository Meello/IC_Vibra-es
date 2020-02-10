using IcVibrations.Common.Profiles;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Mapper;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam;

using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamVibration
{
    /// <summary>
    /// It's responsible to calculate the beam vibration at all contexts.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamVibration<TProfile> : OperationBase<CalculateBeamVibrationRequest<TProfile>, CalculateBeamVibrationResponse>, ICalculateBeamVibration<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// It's responsible to validate the profile.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public abstract bool ValidateProfile(TProfile profile);

        /// <summary>
        /// It's responsible to calculate the geometric properties for each profile.
        /// </summary>
        /// <param name="profile"></param>
        public abstract void CalculateGeometricProperties(TProfile profile);
        
        private readonly INewmarkMethod _newmarkMethod;
        private readonly IMappingResolver _mappingResolver;

        public CalculateBeamVibration(
            INewmarkMethod newmarkMethod,
            IMappingResolver mappingResolver)
        {
            this._newmarkMethod = newmarkMethod;
            this._mappingResolver = mappingResolver;
        }

        protected override async Task<CalculateBeamVibrationResponse> ProcessOperation(CalculateBeamVibrationRequest<TProfile> request)
        {
            CalculateBeamVibrationResponse response = new CalculateBeamVibrationResponse();

            // Tornar input abstrato.
            NewmarkMethodInput input = this._newmarkMethod.CreateInput();

            NewmarkMethodOutput output = await this._newmarkMethod.CreateOutput(input, response);

            response.Data = this._mappingResolver.BuildFrom(output, request.Author, request.AnalysisExplanation);

            return response;
        }

        protected override Task<CalculateBeamVibrationResponse> ValidateOperation(CalculateBeamVibrationRequest<TProfile> request)
        {
            throw new NotImplementedException();
        }
    }
}
