using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IcVibrations.Core.Operations.Piezoelectric.Calculate;
using IcVibrations.DataContracts.Piezoelectric;
using IcVibrations.DataContracts.Piezoelectric.Calculate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IC_Vibrations.Controllers
{
    [Route("api/v1/piezoelectric")]
    public class PiezoelectricController : ControllerBase
    {
        private readonly ICalculatePiezoelectricVibration _calculatePiezoelectric;

        public PiezoelectricController(ICalculatePiezoelectricVibration calculatePiezoelectric)
        {
            this._calculatePiezoelectric = calculatePiezoelectric;
        }

        [HttpGet]
        public ActionResult<CalculatePiezoelectricResponse> Calculate(CalculatePiezoelectricRequestData requestData)
        {
            CalculatePiezoelectricRequest request = new CalculatePiezoelectricRequest(requestData);
            CalculatePiezoelectricResponse response = this._calculatePiezoelectric.Process(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
