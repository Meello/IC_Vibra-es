using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IcVibrations.Core.Operations;
using IcVibrations.Core.Operations.BeamVibration.Calculate;
using IcVibrations.DataContracts;
using IcVibrations.DataContracts.Beam;
using IcVibrations.DataContracts.Beam.Calculate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IC_Vibrations.Controllers
{
    [Route("api/v1/beam")]
    public class BeamController : ControllerBase
    {
        private readonly IOperationBase<CalculateBeamRequest<RectangularBeamRequestData>, CalculateBeamResponse> _rectangularCalculateBeamVibration;

        private readonly IOperationBase<CalculateBeamRequest<CircularBeamRequestData>, CalculateBeamResponse> _circularCalculateBeamVibration;

        public BeamController(
            IOperationBase<CalculateBeamRequest<RectangularBeamRequestData>, CalculateBeamResponse> rectangularCalculateBeamVibration,
            IOperationBase<CalculateBeamRequest<CircularBeamRequestData>, CalculateBeamResponse> circularCalculateBeamVibration)
        {
            this._rectangularCalculateBeamVibration = rectangularCalculateBeamVibration;
            this._circularCalculateBeamVibration = circularCalculateBeamVibration;
        }

        [HttpPost("rectangular")]
        public ActionResult<CalculateBeamResponse> CalculateRectangular(RectangularBeamRequestData beamData, NewmarkMethodParameter methodParameterData)
        {
            var request = new CalculateBeamRequest<RectangularBeamRequestData>(beamData, methodParameterData);
            CalculateBeamResponse response = this._rectangularCalculateBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("circular")]
        public ActionResult<CalculateBeamResponse> CalculateCircular(CircularBeamRequestData beamData, NewmarkMethodParameter methodParameterData)
        {
            var request = new CalculateBeamRequest<CircularBeamRequestData>(beamData, methodParameterData);
            CalculateBeamResponse response = this._circularCalculateBeamVibration.Process(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
