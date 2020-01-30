using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using System.Linq;

namespace IcVibrations.Core.Validators.DynamicVibrationAbsorber
{
    public class DvaValidator : IDvaValidator
    {
        public void Execute(BeamWithDvaRequestData requestData, OperationResponseBase response)
        {
            if (requestData.DvaHardnesses.Count() <= 0 || requestData.DvaHardnesses.Count() > requestData.ElementCount)
            {
                response.AddError("009", $"Invalid number of dva hardnesses: {requestData.DvaHardnesses.Count()}. Min: 0. Max: {requestData.DvaHardnesses}.");
            }
            else if (requestData.DvaHardnesses.Contains(0))
            {
                response.AddError("010", "Dva hardnesses can't contain value zero.");
            }

            if (requestData.DvaMasses.Count() <= 0 || requestData.DvaMasses.Count() > requestData.ElementCount)
            {
                response.AddError("009", $"Invalid number of dva masses: {requestData.DvaMasses.Count()}. Min: 0. Max: {requestData.DvaMasses}.");
            }
            else if (requestData.DvaMasses.Contains(0))
            {
                response.AddError("010", "Dva masses can't contain value zero.");
            }

            if (requestData.DvaNodePositions.Count() <= 0 || requestData.DvaNodePositions.Count() > requestData.ElementCount)
            {
                response.AddError("009", $"Invalid number of dva positions: {requestData.DvaNodePositions.Count()}. Min: 0. Max: {requestData.DvaNodePositions}.");
            }
            else if (requestData.DvaNodePositions.Contains((uint)0) || requestData.DvaNodePositions.Contains(requestData.ElementCount + 1))
            {
                response.AddError("010", $"Dva positions must be in the interval: [0...{requestData.ElementCount}].");
            }
        }
    }
}
