using IcVibrations.Common.Profiles;
using IcVibrations.Models.Beam.Characteristics;

namespace IcVibrations.Models.Beam
{
    public class Beam
    {
        public uint NumberOfElements { get; set; }

        public Material Material { get; set; }

        public GeometricProperty GeometricProperty { get; set; }

        public Fastening FirstFastening { get; set; }

        public Fastening LastFastening { get; set; }

        public double Length { get; set; }

        public double[] Forces { get; set; }
    }
}
