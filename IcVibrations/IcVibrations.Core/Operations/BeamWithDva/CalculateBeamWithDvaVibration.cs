using IcVibrations.Common;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Mapper.Profiles;
using IcVibrations.Core.Models.BeamWithDynamicVibrationAbsorber;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Methods.NewmarkMethod;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Core.Operations.BeamWithDva
{
    /// <summary>
    /// It's responsible to calculate the vibration in a beam with dynamic vibration absorber.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateBeamWithDvaVibration<TProfile> : CalculateVibration<CalculateBeamWithDvaVibrationRequest<TProfile>, BeamWithDvaRequestData<TProfile>, TProfile, BeamWithDva<TProfile>>
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
        public CalculateBeamWithDvaVibration(
            INewmarkMethod<BeamWithDva<TProfile>, TProfile> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<TProfile> profileValidator, 
            IAuxiliarOperation auxiliarOperation,
            IProfileMapper<TProfile> profileMapper) 
            : base(newmarkMethod, mappingResolver, profileValidator, auxiliarOperation)
        {
            this._mappingResolver = mappingResolver;
            this._profileMapper = profileMapper;
        }

        public async override Task<BeamWithDva<TProfile>> BuildBeam(CalculateBeamWithDvaVibrationRequest<TProfile> request, uint degreesFreedomMaximum)
        {
            if (request == null)
            {
                return null;
            }

            int i = 0;
            
            double[] dvaMasses = new double[request.BeamData.Dvas.Count];
            double[] dvaHardnesses = new double[request.BeamData.Dvas.Count];
            uint[] dvaNodePositions = new uint[request.BeamData.Dvas.Count];

            foreach (DynamicVibrationAbsorber dva in request.BeamData.Dvas)
            {
                dvaMasses[i] = dva.DvaMass;
                dvaHardnesses[i] = dva.DvaHardness;
                dvaNodePositions[i] = dva.DvaNodePosition;
                i += 1;
            }

            return new BeamWithDva<TProfile>()
            {
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = await this._mappingResolver.BuildFrom(request.BeamData.Forces, degreesFreedomMaximum),
                GeometricProperty = await this._profileMapper.Execute(request.BeamData.Profile, degreesFreedomMaximum),
                LastFastening = FasteningFactory.Create(request.BeamData.LastFastening),
                Length = request.BeamData.Length,
                Material = MaterialFactory.Create(request.BeamData.Material),
                NumberOfElements = request.BeamData.NumberOfElements,
                Profile = request.BeamData.Profile,
                DvaHardnesses = dvaHardnesses,
                DvaMasses = dvaMasses,
                DvaNodePositions = dvaNodePositions
            };
        }
    }
}
