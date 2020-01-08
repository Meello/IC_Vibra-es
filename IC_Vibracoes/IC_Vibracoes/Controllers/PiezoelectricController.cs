using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IC_Vibrations.Core.Operations.Piezoelectric.Calculate;
using IC_Vibrations.DataContract.Beam;
using IC_Vibrations.DataContract.Beam.CalculatePiezoelectric;
using IC_Vibrations.DataContract.Piezoelectric;
using IC_Vibrations.DataContract.Piezoelectric.Calculate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IC_Vibrations.Controllers
{
    [Route("api/v1/piezoelectric")]
    public class PiezoelectricController : ControllerBase
    {
        private readonly ICalculatePiezoelectricVibration _calculatePiezoelectric;

        //public PiezoelectricController(ICalculatePiezoelectricVibration calculatePiezoelectric)
        //{
        //    this._calculatePiezoelectric = calculatePiezoelectric;
        //}

        [HttpGet]
        public ActionResult<CalculatePiezoelectricResponse> Calculate(PiezoelectricRequestData requestData)
        {
            CalculatePiezoelectricRequest request = new CalculatePiezoelectricRequest(requestData);
            //mudar para receber um arquivo
            CalculatePiezoelectricResponse response = this._calculatePiezoelectric.Process(request);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
