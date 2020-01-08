using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IcVibrations.Core.Operations.Beam.Calculate;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IC_Vibrations.Controllers
{
    [Route("api/v1/beam")]
    public class BeamController : ControllerBase
    {
        private readonly ICalculateBeamVibration _calculateBeamVibration;

        public BeamController(ICalculateBeamVibration calculateBeamVibration)
        {
            this._calculateBeamVibration = calculateBeamVibration;
        }

        [HttpPost]
        public ActionResult<CalculateBeamResponse> Calculate(BeamRequestData requestData)
        {
            CalculateBeamRequest request = new CalculateBeamRequest(requestData);
            CalculateBeamResponse response = this._calculateBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
