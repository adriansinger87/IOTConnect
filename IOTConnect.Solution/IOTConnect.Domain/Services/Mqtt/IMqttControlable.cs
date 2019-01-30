using IOTConnect.Domain.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Services.Mqtt
{
    public interface IMqttControlable
    {
        bool CreateClient(ConfigBase config);

        bool Publish(string topic, string message);

        bool Connect();

        bool Disconnect();

        // -- Properties

        ConfigBase Config { get; }

        // events

        event MqttMessageReceivedEventHandler MessageReceived;
        event MqttConnectedEventHandler Connected;
    }
}
