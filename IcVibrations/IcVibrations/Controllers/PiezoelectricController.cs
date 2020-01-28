using IcVibrations.Core.Operations.PiezoelectricVibration.Calculate;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using Microsoft.AspNetCore.Mvc;

namespace IC_Vibrations.Controllers
{
    [Route("api/v1/piezoelectric")]
    public class PiezoelectricController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CalculatePiezoelectricResponse> Calculate(
            [FromServices] ICalculatePiezoelectricVibration calculatePiezoelectricVibration,
            [FromBody] CalculatePiezoelectricRequest request)
        {
            CalculatePiezoelectricResponse response = calculatePiezoelectricVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
