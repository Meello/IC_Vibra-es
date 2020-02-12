using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Core.NewmarkNumericalIntegration;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.CalculateVibration.BeamWithPiezoelectric;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with piezoelectric.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamWithPiezoelectricVibration<TProfile> : CalculateVibration<CalculateBeamWithPiezoelectricVibrationRequest<TProfile>, PiezoelectricRequestData<TProfile>, TProfile, BeamWithPiezoelectric<TProfile>>
        where TProfile : Profile, new()
    {
        private readonly IMappingResolver _mappingResolver;
        private readonly IProfileMapper<TProfile> _profileMapper;

        /// <summary>
        /// Class construtor.
        /// </summary>
        /// <param name="newmarkMethod"></param>
        /// <param name="mappingResolver"></param>
        /// <param name="profileValidator"></param>
        /// <param name="auxiliarOperation"></param>
        public CalculateBeamWithPiezoelectricVibration(
            INewmarkMethod<BeamWithPiezoelectric<TProfile>, TProfile> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<TProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<TProfile> profileMapper) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._profileMapper = profileMapper;
        }

        public async override Task<BeamWithPiezoelectric<TProfile>> BuildBeam(CalculateBeamWithPiezoelectricVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if(request == null)
            {
                return null;
            }

            return new BeamWithPiezoelectric<TProfile>()
            {
                DielectricConstant = request.BeamData.DielectricConstant,
                DielectricPermissiveness = request.BeamData.DielectricPermissiveness,
                ElasticityConstant = request.BeamData.ElasticityConstant,
                ElementsWithPiezoelectric = request.BeamData.ElementsWithPiezoelectric,
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                PiezoelectricConstant = request.BeamData.PiezoelectricConstant,
                PiezoelectricProfile = request.BeamData.PiezoelectricProfile,
                Profile = request.BeamData.Profile,
                PiezoelectricSpecificMass = request.BeamData.PiezoelectricSpecificMass,
                YoungModulus = request.BeamData.PiezoelectricYoungModulus
            };
        }
    }
}
