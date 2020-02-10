using IcVibrations.Common.Profiles;
using IcVibrations.Core.Mapper;
using IcVibrations.Core.Operations.Profiles;
using IcVibrations.Core.Validators.Profiles;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.Methods.NewmarkMethod;

namespace IcVibrations.Core.Operations.Beam
{
    public abstract class CalculateBeamVibration<TProfile> : CalculateVibration<CalculateBeamVibrationRequest<TProfile>, TProfile, Models.Beam.Beam>, ICalculateBeamVibration<TProfile>
        where TProfile : Profile
    {
        private readonly IProfileBuilder<TProfile> _profileBuilder;

        public CalculateBeamVibration(
            INewmarkMethod<Models.Beam.Beam> newmarkMethod, 
            IMappingResolver mappingResolver, 
            IProfileValidator<TProfile> profileValidator,
            IProfileBuilder<TProfile> profileBuilder) 
            : base(newmarkMethod, mappingResolver, profileValidator)
        {
            this._profileBuilder = profileBuilder;
        }

        public override Models.Beam.Beam BuildBeam(CalculateBeamVibrationRequest<TProfile> request)
        {
            if(request == null)
            {
                return null;
            }



            return new Models.Beam.Beam()
            { 
                FirstFastening = FasteningFactory.Create(request.BeamData.FirstFastening),
                Forces = default,
                GeometricProperty = this._profileBuilder.Execute(request.BeamData.Profile, )
            };
        }
    }
}
