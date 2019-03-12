using IOTConnect.Domain.System.Logging;
using IOTConnect.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IOTConnect.WebAPI.Controllers.Api
{
    /*
     * TODO @ AP: die Konvention ist, dass ein Api-Controller den Zugriff auf eine Resource regelt und keine Funktionalität beschreibt
     * - Besser: einen "SensorsController" (plural) anstelle von "SensorRetrieve.." und "SensorRegistration.."
     * - Die Http Methoden GET, POST, PUT & DELETE regeln dann alles, was man mit der Liste der Sensoren machen kann 
     * - Plural (Sensors) deshalb, da die Ressource serverseitig eine Liste ist. Der Zugriff per Get auf einen Sensor geht dann mittels "Sensors/{id}"
     * -> Diese Schreibweise ist eine Art best practise, wenn es um RESTful Web-APIs geht.
     */
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        // GET: api/Sensors
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sensors/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<controller>/5
        //[HttpGet("GetAllSensorRecords")]
        //public JsonResult GetAllSensorRecords()
        //{
        //    return Json(SensorRegistration.getInstance().getAllSensors());
        //}

        // POST: api/Sensors
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost]
        public DeviceRegistrationReply RegisterSensor(Device sensorreg)
        {
            Log.Info("In registerSensor");
            DeviceRegistrationReply senregreply = new DeviceRegistrationReply();
            DeviceRegistration.getInstance().Add(sensorreg);
            senregreply.ID = sensorreg.ID;
            senregreply.Name = sensorreg.Name;
            senregreply.Timestamp = sensorreg.DateAndTime.Millisecond;
            senregreply.SensorValue = sensorreg.SensorValue;
            senregreply.RegistrationStatus = "Succesfull";

            return senregreply;
        }

        [HttpPost("InsertSensor")]
        public IActionResult InsertSensors(Device sensorreg)
        {
            Log.Info("In registerSensor");
            DeviceRegistrationReply senregreply = new DeviceRegistrationReply();
            DeviceRegistration.getInstance().Add(sensorreg);
            senregreply.ID = sensorreg.ID;
            senregreply.Name = sensorreg.Name;
            // senregreply.Timestamp = sensorreg.Timestamp;
            senregreply.SensorValue = sensorreg.SensorValue;
            senregreply.RegistrationStatus = "Succesfull";


            return Ok(senregreply);
        }

        //[Route("sensor/")]
        //[HttpPost("AddSensor")]
        //public JsonResult AddSensor(Sensor sensorreg)
        //{
        //    Log.Info("In registerSensor");
        //    SensorRegistrationReply senregreply = new SensorRegistrationReply();
        //    SensorRegistration.getInstance().Add(sensorreg);
        //    senregreply.ID = sensorreg.ID;
        //    senregreply.Name = sensorreg.Name;
        //    // senregreply.Timestamp = sensorreg.Timestamp;
        //    senregreply.SensorValue = sensorreg.SensorValue;
        //    senregreply.RegistrationStatus = "Succesfull";

        //    return Json(senregreply);
        //}


        // PUT: api/Sensors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //public JsonResult UpdateSensorRecord(Sensor sensorN)
        //{
        //    Log.Info("In updateSensorRecord");
        //    return Json(SensorRegistration.getInstance().UpdateSensor(sensorN));
        //}


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpDelete]
        public IActionResult DeleteSensorRecord(String sensorID)
        {
            Log.Info("In deleteSensorRecord");
            return Ok(DeviceRegistration.getInstance().Remove(sensorID));
        }
    }
}
