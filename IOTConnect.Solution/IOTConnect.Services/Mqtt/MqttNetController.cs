using IOTConnect.Domain.Config;
using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IOTConnect.Services.Mqtt
{
    public class MqttNetController : IMqttControlable
    {
        // -- fields

        private IMqttClient _client;
        private MqttConfig _config;
        private IMqttClientOptions _options;

        public event MqttMessageReceivedEventHandler MessageReceived;
        public event MqttConnectedEventHandler Connected;
        public event MqttConnectedEventHandler Disconnected;

        // -- constructor

        public MqttNetController()
        {

        }

        // -- methods

        public bool CreateClient(ConfigBase config)
        {
            _config = config as MqttConfig;
            _options = new MqttClientOptionsBuilder()
               .WithTcpServer(_config.Broker, _config.Port)
               .WithClientId(_config.ClientID)
               .WithCleanSession(true)
               .Build();

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            //_client.Connected += mqttConnected;
            _client.Connected += async (obj, e) =>
            {
                // fire own event for application layer
                Connected?.Invoke(this, new MqttConnectedEventArgs(_config.Broker, _config.Port, _config.ClientID));

                // bind all topics to the connected broker
                var client = (IMqttClient)obj;
                foreach (string topic in _config.Topics)
                {
                    await client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
                }
            };

            _client.Disconnected += async (s, e) =>
            {
                // fire own event for application layer
                Disconnected?.Invoke(this, new MqttConnectedEventArgs(_config.Broker, _config.Port, _config.ClientID));

                // reconnect after 5 seconds
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await _client.ConnectAsync(_options);
                }
                catch (Exception ex)
                {
                    Log.Error("could not reconnect", Sources.Mqtt);
                    Log.Fatal(ex);
                }
            };

            _client.ApplicationMessageReceived += (s, e) =>
            {
                Log.Trace(e.ApplicationMessage.Topic, Sources.Mqtt);
                string topic = e.ApplicationMessage.Topic;
                string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                MessageReceived?.Invoke(this, new MqttReceivedEventArgs(topic, msg));
            };

            return true;
        }

        public async Task ConnectAsync()
        {
            await _client.ConnectAsync(_options);
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }

        public Task PublishAsync(string topic, string message)
        {
            return PublishAsync(topic, message, _config.QoS);
        }

        public async Task PublishAsync(string topic, string message, int qos)
        {
            MqttApplicationMessage msg = new MqttApplicationMessageBuilder()
               .WithTopic(topic)
               .WithPayload(message)
               .Build();

            if (qos >= 0 &&
                qos <= 2)
            {
                msg.QualityOfServiceLevel = (MqttQualityOfServiceLevel)qos;
            }
            else
            {
                msg.QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            }
            
            await _client.PublishAsync(msg);
        }



        // -- properties

        public ConfigBase Config { get { return _config; } }

    }
}
