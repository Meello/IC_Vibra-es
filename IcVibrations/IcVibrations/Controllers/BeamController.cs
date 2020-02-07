using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using IcVibrations.DataContracts.Beam.CalculateBeamWithDynamicVibrationAbsorber;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IC_Vibrations.Controllers
{

    [Route("api/v1/beam")]
    public class BeamController : ControllerBase
    {
        [HttpPost("rectangular")]
        public async Task<ActionResult<CalculateBeamResponse>> Calculate(
            [FromServices] AbstractCalculateBeamVibration<RectangularBeamRequestData> calculateRectangularBeamVibration, 
            [FromBody] CalculateBeamRequest<RectangularBeamRequestData> request)
        {
            CalculateBeamResponse response = await calculateRectangularBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular")]
        public async Task<ActionResult<CalculateBeamResponse>> Calculate(
            [FromServices] AbstractCalculateBeamVibration<CircularBeamRequestData> calculateCircularBeamVibration,
            [FromBody] CalculateBeamRequest<CircularBeamRequestData> request)
        {
            CalculateBeamResponse response = await calculateCircularBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular/dynamic-vibration-absorber")]
        public async Task<ActionResult<CalculateBeamResponse>> Calculate(
            [FromServices] AbstractCalculateBeamVibration<CircularBeamWithDvaRequestData> calculateCircularBeamWithDvaVibration,
            [FromBody] CalculateBeamWithDvaRequest<CircularBeamWithDvaRequestData> request)
        {
            CalculateBeamResponse response = await calculateCircularBeamWithDvaVibration.Process(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}