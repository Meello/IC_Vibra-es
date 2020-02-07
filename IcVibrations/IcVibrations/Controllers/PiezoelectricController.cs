using IcVibrations.Core.Operations.PiezoelectricVibration.Calculate;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IC_Vibrations.Controllers
{
    [Route("api/v1/piezoelectric")]
    public class PiezoelectricController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CalculatePiezoelectricResponse>> Calculate(
            [FromServices] ICalculatePiezoelectricVibration calculatePiezoelectricVibration,
            [FromBody] CalculatePiezoelectricRequest request)
        {
            CalculatePiezoelectricResponse response = await calculatePiezoelectricVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
