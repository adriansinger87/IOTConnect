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
    public class SensorRegistrationController : Controller
    {
        // POST api/<controller>
        [HttpPost]
        public SensorRegistrationReply RegisterSensor (Sensor sensorreg)
        {
            Console.WriteLine("In registerSensor");
            SensorRegistrationReply senregreply = new SensorRegistrationReply();
            SensorRegistration.getInstance().Add(sensorreg);
            senregreply.ID = sensorreg.ID;
            senregreply.Name = sensorreg.Name;
            senregreply.Timestamp = sensorreg.Timestamp;
            senregreply.SensorValue = sensorreg.SensorValue;
            senregreply.RegistrationStatus = "Succesfull";

            return senregreply;
        }

        [HttpPost("InsertSensor")]
        public IActionResult InsertSensors(Sensor sensorreg)
        {
            Console.WriteLine("In registerSensor");
            SensorRegistrationReply senregreply = new SensorRegistrationReply();
            SensorRegistration.getInstance().Add(sensorreg);
            senregreply.ID = sensorreg.ID;
            senregreply.Name = sensorreg.Name;
            senregreply.Timestamp = sensorreg.Timestamp;
            senregreply.SensorValue = sensorreg.SensorValue;
            senregreply.RegistrationStatus = "Succesfull";


            return Ok(senregreply);
        }

        [Route("sensor/")]
        [HttpPost("AddStudent")]
        public JsonResult AddSensor(Sensor sensorreg)
        {
            Console.WriteLine("In registerSensor");
            SensorRegistrationReply senregreply = new SensorRegistrationReply();
            SensorRegistration.getInstance().Add(sensorreg);
            senregreply.ID = sensorreg.ID;
            senregreply.Name = sensorreg.Name;
            senregreply.Timestamp = sensorreg.Timestamp;
            senregreply.SensorValue = sensorreg.SensorValue;
            senregreply.RegistrationStatus = "Succesfull";

            return Json(senregreply);
        }

    }

}
