using IcVibrations.Common.Profiles;
using IcVibrations.Core.Operations.Beam.Circular;
using IcVibrations.Core.Operations.Beam.Rectangular;
using IcVibrations.Core.Operations.BeamWithDva;
using IcVibrations.Core.Operations.BeamWithPiezoelectric;
using IcVibrations.DataContracts.CalculateVibration;
using IcVibrations.DataContracts.CalculateVibration.Beam;
using IcVibrations.DataContracts.CalculateVibration.BeamWithDynamicVibrationAbsorber;
using IcVibrations.DataContracts.CalculateVibration.BeamWithPiezoelectric;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IC_Vibrations.Controllers
{

    [Route("api/v1/beam")]
    public class VibrationController : ControllerBase
    {
        [HttpPost("rectangular")]
        public async Task<ActionResult<CalculateVibrationResponse>> Calculate(
            [FromServices] ICalculateRectangularBeamVibration calculateVibration,
            [FromBody] CalculateBeamVibrationRequest<RectangularProfile> request)
        {
            CalculateVibrationResponse response = await calculateVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("rectangular/dynamic-vibration-absorber")]
        public async Task<ActionResult<CalculateVibrationResponse>> Calculate(
            [FromServices] CalculateRectangularBeamWithDvaVibration calculateVibration,
            [FromBody] CalculateBeamWithDvaVibrationRequest<RectangularProfile> request)
        {
            CalculateVibrationResponse response = await calculateVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("rectangular/piezoelectric")]
        public async Task<ActionResult<CalculateVibrationResponse>> Calculate(
            [FromServices] CalculateRectangularBeamWithPiezoelectricVibration calculateVibration,
            [FromBody] CalculateBeamWithPiezoelectricVibrationRequest<RectangularProfile> request)
        {
            CalculateVibrationResponse response = await calculateVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular")]
        public async Task<ActionResult<CalculateVibrationResponse>> Calculate(
            [FromServices] ICalculateCircularBeamVibration calculateVibration,
            [FromBody] CalculateBeamVibrationRequest<CircularProfile> request)
        {
            CalculateVibrationResponse response = await calculateVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular/dynamic-vibration-absorber")]
        public async Task<ActionResult<CalculateVibrationResponse>> Calculate(
            [FromServices] CalculateCircularBeamWithDvaVibration calculateVibration,
            [FromBody] CalculateBeamWithDvaVibrationRequest<CircularProfile> request)
        {
            CalculateVibrationResponse response = await calculateVibration.Process(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular/piezoelectric")]
        public async Task<ActionResult<CalculateVibrationResponse>> Calculate(
            [FromServices] CalculateCircularBeamWithPiezoelectricVibration calculateVibration,
            [FromBody] CalculateBeamWithPiezoelectricVibrationRequest<CircularProfile> request)
        {
            CalculateVibrationResponse response = await calculateVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}