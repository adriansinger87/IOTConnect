using IOTConnect.Domain.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IOTConnect.Domain.Services.Mqtt
{
    public interface IMqttControlable
    {
        bool CreateClient(ConfigBase config);

        Task PublishAsync(string topic, string message);
        Task PublishAsync(string topic, string message, int qos);

        Task ConnectAsync();

        Task DisconnectAsync();

        // -- Properties

        ConfigBase Config { get; }

        // events

        event MqttMessageReceivedEventHandler MessageReceived;
        event MqttConnectedEventHandler Connected;
        event MqttConnectedEventHandler Disconnected;
    }
}
