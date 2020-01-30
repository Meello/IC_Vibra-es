using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using Microsoft.AspNetCore.Mvc;

namespace IC_Vibrations.Controllers
{

    [Route("api/v1/beam")]
    public class BeamController : ControllerBase
    {
        [HttpPost("rectangular")]
        public ActionResult<CalculateBeamResponse> Calculate(
            [FromServices] AbstractCalculateBeamVibration<RectangularBeamRequestData> calculateRectangularBeamVibration, 
            [FromBody] CalculateBeamRequest<RectangularBeamRequestData> request)
        {
            CalculateBeamResponse response = calculateRectangularBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular")]
        public ActionResult<CalculateBeamResponse> Calculate(
            [FromServices] AbstractCalculateBeamVibration<CircularBeamRequestData> calculateCircularBeamVibration,
            [FromBody] CalculateBeamRequest<CircularBeamRequestData> request)
        {
            CalculateBeamResponse response = calculateCircularBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular/dynamic-vibration-absorber")]
        public ActionResult<CalculateBeamResponse> Calculate(
            [FromServices] AbstractCalculateBeamVibration<CircularBeamWithDvaRequestData> calculateCircularBeamWithDvaVibration,
            [FromBody] CalculateBeamWithDvaRequest<CircularBeamWithDvaRequestData> request)
        {
            CalculateBeamResponse response = calculateCircularBeamWithDvaVibration.Process(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
