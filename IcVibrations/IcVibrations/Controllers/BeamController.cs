using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using Microsoft.AspNetCore.Mvc;

namespace IC_Vibrations.Controllers
{

    [Route("api/v1/beam")]
    public class BeamController : ControllerBase
    {
        [HttpPost("rectangular")]
        public ActionResult<CalculateBeamResponse> CalculateRectangular(
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
        public ActionResult<CalculateBeamResponse> CalculateCircular(
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
    }
}
