using IOTConnect.Domain.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Services.Mqtt
{
    public delegate void MqttMessageReceivedEventHandler(object sender, MqttReceivedEventArgs e);

    public class MqttReceivedEventArgs : EventArgs
    {

        // -- Constructor

        public MqttReceivedEventArgs(string topic, string message)
        {
            Topic = topic;
            Message = message;
        }

        public T Deserialize<T>(IDeserializable deserializer) where T : new()
        {
            return deserializer.Deserialize<T>(Message);
        }

        // -- properties

        public string Topic { get; private set; }

        public string Message { get; private set; }

    }
}
