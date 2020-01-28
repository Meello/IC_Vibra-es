using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.DTO;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.Methods.AuxiliarOperations;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;
using static IcVibrations.Common.Enum;

namespace IcVibrations.Core.Mapper
{
    public class FasteningFactory
    {
        public static Fastening Create(string fastening)
        {
            switch ((Fastenings)Enum.Parse(typeof(Fastenings), fastening, ignoreCase: true))
            {
                case Fastenings.Fixed : return new Fixed();
                case Fastenings.Pinned : return new Pinned();
                case Fastenings.Simple: return new Simple();
            }

            throw new Exception();
        }
    }

    public class MaterialFactory
    {
        public static Material Create(string material)
        {
            switch ((Materials)Enum.Parse(typeof(Materials), material, ignoreCase: true))
            {
                case Materials.Steel1020: return new Steel1020();
                case Materials.Steel4130: return new Steel4130();
            }

            throw new Exception();
        }
    }

    public class MappingResolver : IMappingResolver
    {
        public Beam BuildFrom(CircularBeamRequestData circularBeamRequestData)
        {
            if (circularBeamRequestData == null)
            {
                return null;
            }

            double[] force = new double[(circularBeamRequestData.ElementCount + 1) * Constants.DegreesFreedom];

            for (int i = 0; i < circularBeamRequestData.Forces.Length; i++)
            {
                force[2 * circularBeamRequestData.ForceNodePositions[i]] = circularBeamRequestData.Forces[i];
            }

            return new Beam
            {
                ElementCount = circularBeamRequestData.ElementCount,
                FirstFastening = FasteningFactory.Create(circularBeamRequestData.FirstFastening),
                Forces = force,
                LastFastening = FasteningFactory.Create(circularBeamRequestData.LastFastening),
                Length = circularBeamRequestData.Length,
                Material = MaterialFactory.Create(circularBeamRequestData.Material),
                Profile = new CircularProfile
                {
                    Area = default,
                    Diameter = circularBeamRequestData.Diameter,
                    MomentInertia = default,
                    Thickness = circularBeamRequestData.Thickness
                }
            };
        }

        public Beam BuildFrom(RectangularBeamRequestData beamRequestData)
        {
            if (beamRequestData == null)
            {
                return null;
            }

            double[] force = new double[(beamRequestData.ElementCount + 1) * Constants.DegreesFreedom];

            for (int i = 0; i < beamRequestData.Forces.Length; i++)
            {
                force[2 * beamRequestData.ForceNodePositions[i]] = beamRequestData.Forces[i];
            }

            return new Beam
            {
                ElementCount = beamRequestData.ElementCount,
                FirstFastening = FasteningFactory.Create(beamRequestData.FirstFastening),
                Forces = force,
                LastFastening = FasteningFactory.Create(beamRequestData.LastFastening),
                Length = beamRequestData.Length,
                Material = MaterialFactory.Create(beamRequestData.Material),
                Profile = new RectangularProfile
                {
                    Area = default,
                    Height = beamRequestData.Height,
                    MomentInertia = default,
                    Thickness = beamRequestData.Thickness,
                    Width = beamRequestData.Width
                }
            };
        }

        public Piezoelectric BuildFrom(PiezoelectricRequestData piezoelectricRequestData)
        {
            if(piezoelectricRequestData == null)
            {
                return null;
            }

            return new Piezoelectric
            {
                DielectricConstant = piezoelectricRequestData.DielectricConstant,
                DielectricPermissiveness = piezoelectricRequestData.DielectricPermissiveness,
                ElasticityConstant = piezoelectricRequestData.ElasticityConstant,
                ElementLength = piezoelectricRequestData.ElementLength,
                ElementsWithPiezoelectric = piezoelectricRequestData.ElementsWithPiezoelectric,
                PiezoelectricConstant = piezoelectricRequestData.PiezoelectricConstant,
                Profile = new RectangularProfile
                {
                    Area = default,
                    Height = piezoelectricRequestData.Height,
                    MomentInertia = default,
                    Thickness = piezoelectricRequestData.Thickness,
                    Width = piezoelectricRequestData.Width
                },
                SpecificMass = piezoelectricRequestData.SpecificMass,
                YoungModulus = piezoelectricRequestData.YoungModulus
            };
        }

        public OperationResponseData BuildFrom(NewmarkMethodOutput output)
        {
            if(output == null)
            {
                return null;
            }

            return new OperationResponseData
            {
                Result = output.Result,
                Time = output.Time
            };
        }
    }
}
