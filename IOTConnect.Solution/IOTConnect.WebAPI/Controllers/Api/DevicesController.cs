using IOTConnect.Application.Devices;
using IOTConnect.Application.Repository;
using IOTConnect.Application.Values;
using IOTConnect.Domain.IO;
using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Enumerations;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.IO.Adapters;
using IOTConnect.Persistence.IO.Settings;
using IOTConnect.Services.Mqtt;
using IOTConnect.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        // -- fields

        private IMqttControlable _mqtt;

        // -- constructor

        public DevicesController(IMqttControlable mqtt)
        {
            _mqtt = mqtt;
        }

        // GET: api/devices
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return (_mqtt.Config as MqttConfig).Topics;
        }

        // GET: api/devices/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // POST: api/devices
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        // PUT: api/devices/5
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
