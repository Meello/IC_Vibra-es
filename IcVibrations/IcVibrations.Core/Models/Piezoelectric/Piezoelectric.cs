using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Core.Models.Piezoelectric
{
    public class Piezoelectric
    {
        public double ElementLength { get; set; }

        public double DielectricConstantToConstantStrain { get; set; }

        public double SpecificMass { get; set; }

        public double PiezoelectricStress { get; set; }

        public double ElasticityToConstantElectricField { get; set; }

        public RectangularProfile Profile { get; set; }

        public int[] ElementsWithPiezoelectric { get; set; }

        // OLHAR NA TESE DE MESTRADO ==> TEM TUDO O QUE PRECISA LÁ
    }
}
