using IOTConnect.Application.Devices;
using IOTConnect.Application.Values;
using IOTConnect.Domain.IO;
using IOTConnect.Domain.Models.IoT;
using IOTConnect.Domain.System.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IOTConnect.Persistence.IO.Adapters
{
    public class EnilinkToValueStateAdapter : IAdaptable
    {

        private List<EnilinkDevice> _endpoints;
        private EnilinkDevice _lastEndpoint;
        private ValueState _lastValue;

        public EnilinkToValueStateAdapter()
        {
            _endpoints = new List<EnilinkDevice>();
        }

        public Tout Adapt<Tout, Tin>(Tin input) where Tout : new()
        {
            // type checks
            Type outType = typeof(Tout);
            Type inType = typeof(Tin);

            if (typeof(List<EnilinkDevice>).IsAssignableFrom(outType) == false ||
                typeof(string).IsAssignableFrom(inType) == false)
            {
                Log.Error($"The casting assumes the type '{typeof(ValueState).Name}' but '{outType.Name}' was found.");
                return new Tout();
            }

            var json = JsonConvert.DeserializeObject<JObject>(input as string);

            iterateJson(json);

            return (Tout)Convert.ChangeType(_endpoints, outType);
        }

        private void iterateJson(JToken json)
        {
            foreach (JToken child in json.Children())
            {
                if (child.Type == JTokenType.Object)
                {
                    // i.e. voltage
                    JObject obj = child as JObject;

                    if (obj["time"] != null &&
                        obj["value"] != null)
                    {
                        _lastValue = new ValueState();
                        _lastValue.Time = obj["time"].ToString();
                        _lastValue.Value = obj["value"].ToString();
                        _lastEndpoint.Data.Add(_lastValue);
                    }

                    return;
                }
                else if (child.Type == JTokenType.Property)
                {
                    // i.e. aximus
                    JProperty prop = child as JProperty;

                    _lastEndpoint = _endpoints.FirstOrDefault(d => d.Id == prop.Name);

                    if (_lastEndpoint == null)
                    {
                        _lastEndpoint = new EnilinkDevice { Id = prop.Name };
                        _endpoints.Add(_lastEndpoint);
                    }
                }

                iterateJson(child);
            }
        }
    }
}
