using IOTConnect.Application.Devices;
using IOTConnect.Domain.System.Logging;
using IOTConnect.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /*
         * HACK @ AP: Bitte IMMER vor jedem Push prüfen, ob die Projektmappe gebaut werden kann und ob die Tests alle durchlaufen.
         * Sollte mal ein Test schief gehen ist das kein Problem, dann einfach einen Kommentar hinterlassen, dass man an der Sache dran ist.
         * Es sollte nicht passieren, dass andere Team-Kollegen einen Pull machen und erstmal Syntax-Fehler bereinigen müssen und
         * der Programmablauf nicht sichergestellt ist
         * TODO @ AP: Bitte die Methode DevicesController.GetSensorList reparieren
         */
        //[HttpGet("GetSensorList")]
        //public List<SensorDevice> GetSensorList()
        //{
        //    return SensorDevicesModel.;    
        //}

        // GET: api/Sensors/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
 

        // POST: api/Sensors
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }



        // PUT: api/Sensors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
