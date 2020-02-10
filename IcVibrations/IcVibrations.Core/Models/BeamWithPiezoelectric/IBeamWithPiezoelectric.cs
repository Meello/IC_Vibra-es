using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;

namespace IcVibrations.Core.Models.BeamWithPiezoelectric
{
    /// <summary>
    /// It represents a beam with piezoelectric.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    interface IBeamWithPiezoelectric<TProfile> : IBeam<TProfile>
        where TProfile : Profile, new()
    {
        /// <summary>
        /// Piezoelectric Young Modulus.
        /// </summary>
        double YoungModulus { get; set; }

        /// <summary>
        /// Piezoelectric constant. Variable: d31.
        /// </summary>
        double PiezoelectricConstant { get; set; }

        /// <summary>
        /// Dielectric constant. Variable: k33.
        /// </summary>
        double DielectricConstant { get; set; }

        /// <summary>
        /// Dielectric permissiveness. Variable: e31.
        /// </summary>
        double DielectricPermissiveness { get; set; }

        /// <summary>
        /// Elasticity value for constant electric field. Variable: c11.
        /// </summary>
        double ElasticityConstant { get; set; }

        /// <summary>
        /// Piezoelectric specific mass.
        /// </summary>
        double SpecificMass { get; set; }

        /// <summary>
        /// Elements with piezoelectric.
        /// </summary>
        uint[] ElementsWithPiezoelectric { get; set; }

        /// <summary>
        /// Piezoelectric profile.
        /// </summary>
        TProfile PiezoelectricProfile { get; set; }
    }
}
