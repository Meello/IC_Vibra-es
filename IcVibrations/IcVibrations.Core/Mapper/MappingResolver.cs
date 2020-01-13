using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Core.DTO;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.Methods.AuxiliarMethods;
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
        public Beam AddValues(BeamRequestData beamRequestData)
        {
            if (beamRequestData == null)
            {
                return null;
            }

            return new Beam
            {
                ElementCount = beamRequestData.ElementCount,
                FirstFastening = FasteningFactory.Create(beamRequestData.FirstFastening),
                LastFastening = FasteningFactory.Create(beamRequestData.LastFastening),
                Length = beamRequestData.Length,
                Material = MaterialFactory.Create(beamRequestData.Material)
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
