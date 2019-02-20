using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using IOTConnect.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    public class SensorRegistrationController : Controller
    {
        // POST api/<controller>
        [HttpPost]
        public SensorRegistrationReply RegisterSensor (Sensor sensorreg)
        {
            // TODO @ AP: Alle Console.WriteLine entfernen und durch Log.Info(..) oder Log.Debug etc ersetzen

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
