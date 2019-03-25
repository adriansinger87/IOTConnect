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
        private bool _isConnected;
        private MqttConfig _config;
        private IMqttControlable _mqtt;
        private int _duration = 15000;


        // GET: api/devices
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "M40/Sensor0815", "M40/Sensor4711" };
        }

        [HttpGet("GetSensorList")]
        public List<SensorDevice> GetSensorList(MqttConfig _config)
        {
            _config.Broker = "linkedfactory.iwu.fraunhofer.de";
            _config.Port = 8883;
            _config.ClientID = "IOTConnectDemo";
            _config.LasWill = $"Goodby from {_config.ClientID}";
            _config.QoS = 0;


            List<string> Topics = new List<string>();
            Topics.Add("M40/Sensor0815");
            Topics.Add("M40/Sensor4711");
            _config.Topics = Topics;
            _mqtt = new MqttNetController();
            _mqtt.CreateClient(_config);

            var enilinkRepo = new EnilinkRepository();
            var sensorsRepo = new SensorsRepository();

            var valueStates = new List<ValueState>();
            var numDevices = _config.Topics.Count;
            var watch = new Stopwatch();

            _mqtt.MessageReceived += (o, e) =>
            {
                Log.Info($"{e.Topic}: after {watch.ElapsedMilliseconds} ms", Sources.Mqtt);

                if (e.Topic.StartsWith("LF/E"))
                {
                    var dev = enilinkRepo.FirstOrNew(x => x.Id == e.Topic, out bool created);
                    if (created)
                    {
                        dev.Id = e.Topic;
                    }

                    var properties = e.Deserialize<List<EnilinkDevice>>(new EnilinkToValueStateAdapter());
                    dev.AppendProperties(properties);
                }
                else
                {
                    // M4.0 sensors
                    var dev = sensorsRepo.FirstOrNew(x => x.Id == e.Topic, out bool created);
                    if (created)
                    {
                        dev.Id = e.Topic;
                    }
                    var value = e.Deserialize<ValueState>(new JsonToObjectAdapter());
                    dev.Data.Add(value);
                }
            };

           //await ConnectAsync();

            watch.Start();
            while (watch.ElapsedMilliseconds < _duration) { } 
            watch.Stop();
            //await DisconnectAsync();

            var totalDevices = enilinkRepo.Count + sensorsRepo.Count;

            var allValues = new List<ValueState>();
            enilinkRepo.Items.ForEach(x => allValues.AddRange(x.GetAllValues(true)));
            sensorsRepo.Items.ForEach(x => allValues.AddRange(x.Data.ToArray()));


            return sensorsRepo.Items;
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
