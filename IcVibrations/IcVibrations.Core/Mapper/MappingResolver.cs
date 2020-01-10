using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
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
        public Beam AddValues(CircularBeamRequestData circularBeamRequestData)
        {
            if (circularBeamRequestData == null)
            {
                return null;
            }

            return new Beam
            {
                FirstFastening = FasteningFactory.Create(circularBeamRequestData.FirstFastening),
                LastFastening = FasteningFactory.Create(circularBeamRequestData.LastFastening),
                Length = circularBeamRequestData.Length,
                Material = MaterialFactory.Create(circularBeamRequestData.Material),
                Profile = new CircularProfile
                {
                    Thickness = circularBeamRequestData.Thickness,
                    Diameter = circularBeamRequestData.Diameter
                }
            };
        }

        public Beam AddValues(RectangularBeamRequestData rectangularBeamRequestData)
        {
            if (rectangularBeamRequestData == null)
            {
                return null;
            }

            return new Beam
            {
                FirstFastening = FasteningFactory.Create(rectangularBeamRequestData.FirstFastening),
                LastFastening = FasteningFactory.Create(rectangularBeamRequestData.LastFastening),
                Length = rectangularBeamRequestData.Length,
                Material = MaterialFactory.Create(rectangularBeamRequestData.Material),
                Profile = new RectangleProfile
                {
                    Thickness = rectangularBeamRequestData.Thickness,
                    Height = rectangularBeamRequestData.Height,
                    Width = rectangularBeamRequestData.Width
                }
            };
        }

        public void AddValues(BeamMatrix values, Beam local)
        {
            if(values == null)
            {
                return;
            }

            local.Damping = values.Damping;
            local.Hardness = values.Hardness;
            local.Mass = values.Mass;
            local.Profile.Area = values.Area;
            local.Profile.MomentInertia = values.MomentInertia;
        }

        public OperationResponseData BuildFrom(NewmarkMethodOutput output, string analysisExplanation)
        {
            if(output == null || string.IsNullOrWhiteSpace(analysisExplanation))
            {
                return null;
            }

            return new OperationResponseData
            {
                AnalysisExplanation = analysisExplanation,
                Result = output.AnalysisResult,
                Time = output.Time
            };
        }
    }
}
