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
    public class SensorRetriveController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public List<Sensor> Get()
        {
            return SensorRegistration.getInstance().getAllSensors();
        }

        // GET api/<controller>/5
        [HttpGet("GetAllSensorRecords")]
        public JsonResult GetAllSensorRecords()
        {
            return Json(SensorRegistration.getInstance().getAllSensors());
        }
    }
}
