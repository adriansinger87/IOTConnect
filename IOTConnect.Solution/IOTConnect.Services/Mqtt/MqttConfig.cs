using System;
using System.Collections.Generic;
using System.Text;
using IOTConnect.Domain.Config;

namespace IOTConnect.Services.Mqtt
{
    public class MqttConfig : ConfigBase
    {
        // -- constructor

        public MqttConfig() : base ("mqtt-config")
        {
            Broker = "broker.hivemq.com";
            Port = 1883;
            ClientID = "OpcDynamicServer";
            Topics = new List<string>();
        }

        // -- methods

        public override string ToString()
        {
            return $"{ClientID} @ {Broker}:{Port}";
        }

        // -- properties

        public string Broker { get; set; }
        public int Port { get; set; }
        public string ClientID { get; set; }
        public List<string> Topics { get; set; }

    }
}
