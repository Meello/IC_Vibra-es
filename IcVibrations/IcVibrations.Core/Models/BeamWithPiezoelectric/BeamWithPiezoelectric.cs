using IcVibrations.Common.Profiles;

namespace IcVibrations.Core.Models.Piezoelectric
{
    public class BeamWithPiezoelectric<TProfile> : Models.Beam.Beam
        where TProfile : Profile
    {
        /// <summary>
        /// Piezoelectric Young Modulus.
        /// </summary>
        public double YoungModulus { get; set; }

        /// <summary>
        /// Piezoelectric constant. Variable: d31.
        /// </summary>
        public double PiezoelectricConstant { get; set; }

        /// <summary>
        /// Dielectric constant. Variable: k33.
        /// </summary>
        public double DielectricConstant { get; set; }

        /// <summary>
        /// Dielectric permissiveness. Variable: e31.
        /// </summary>
        public double DielectricPermissiveness { get; set; }

        /// <summary>
        /// Elasticity value for constant electric field. Variable: c11.
        /// </summary>
        public double ElasticityConstant { get; set; }

        /// <summary>
        /// Piezoelectric specific mass.
        /// </summary>
        public double SpecificMass { get; set; }

        /// <summary>
        /// Elements with piezoelectric.
        /// </summary>
        public uint[] ElementsWithPiezoelectric { get; set; }

        /// <summary>
        /// Piezoelectric profile.
        /// </summary>
        public TProfile PiezoelectricProfile { get; set; }
    }
}
