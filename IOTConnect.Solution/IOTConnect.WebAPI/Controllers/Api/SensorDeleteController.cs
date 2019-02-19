using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using IOTConnect.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IOTConnect.WebAPI.Controllers.Api
{
    [Route("sensor/remove/{sensorID}")]
    public class SensorDeleteController : Controller
    {
        // DELETE api/<controller>/5
        [HttpDelete]
        public IActionResult DeleteSensorRecord (String sensorID)
        {
            Console.WriteLine("In deleteSensorRecord");
            return Ok(SensorRegistration.getInstance().Remove(sensorID));
        }
    }
}
