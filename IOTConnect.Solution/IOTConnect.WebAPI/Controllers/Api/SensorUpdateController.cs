using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using IOTConnect.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IOTConnect.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    public class SensorUpdateController : Controller
    {
       // PUT api/<controller>/5
        [HttpPut]
        public JsonResult UpdateSensorRecord (Sensor sensorN)
        {
            Console.WriteLine("In updateSensorRecord");
            return Json(SensorRegistration.getInstance().UpdateSensor(sensorN));
        }
    }
}
