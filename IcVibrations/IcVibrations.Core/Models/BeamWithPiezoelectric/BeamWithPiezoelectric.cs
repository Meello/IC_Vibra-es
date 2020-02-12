using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.Models.BeamWithPiezoelectric;
using IcVibrations.Models.Beam.Characteristics;

namespace IcVibrations.Core.Models.Piezoelectric
{
    /// <summary>
    /// It represents a beam with piezoelectric.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class BeamWithPiezoelectric<TProfile> : Beam<TProfile>, IBeamWithPiezoelectric<TProfile>
        where TProfile : Profile, new()
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
        /// Electrical charge on piezoelectric surface.
        /// </summary>
        public double[] ElectricalCharge { get; set; }

        /// <summary>
        /// Piezoelectric specific mass.
        /// </summary>
        public double PiezoelectricSpecificMass { get; set; }

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
