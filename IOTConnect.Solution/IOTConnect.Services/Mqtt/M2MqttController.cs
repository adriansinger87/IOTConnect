using IOTConnect.Domain.Config;
using IOTConnect.Domain.Services.Mqtt;
using System;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IOTConnect.Services.Mqtt
{
    /// <summary>
    /// HACK: do not use this instance in production
    /// </summary>
    public class M2MqttController : IMqttControlable
    {
        private MqttClient _client;
        private MqttConfig _config;

        public event MqttMessageReceivedEventHandler MessageReceived;
        public event MqttConnectedEventHandler Connected;
        public event MqttConnectedEventHandler Disconnected;

        public M2MqttController()
        {

        }

        public bool CreateClient(ConfigBase config)
        {
            _config = config as MqttConfig;
            _client = new MqttClient(_config.Broker, ((int)_config.Port), false, null, null, MqttSslProtocols.None);

            byte[] qosLevels = new byte[_config.Topics.Count];
            for (int i = 0; i < _config.Topics.Count; i++) { qosLevels[i] = MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE; }
            _client.Subscribe(_config.Topics.ToArray(), qosLevels);

            _client.MqttMsgPublishReceived += (object sender, MqttMsgPublishEventArgs e) =>
            {
                string topic = e.Topic;
                string msg = Encoding.UTF8.GetString(e.Message);
                MessageReceived?.Invoke(this, new MqttReceivedEventArgs(topic, msg));
            };

            return true;
        }

        public async Task ConnectAsync()
        {
            await Task.Run(() =>
            {
                _client.Connect(_config.ClientID);
                if (_client.IsConnected)
                {
                    Connected?.Invoke(this, new MqttConnectedEventArgs(_config.Broker, _config.Port, _config.ClientID));
                }
            });

        }

        public async Task DisconnectAsync()
        {
            await Task.Run(() =>
            {
                _client.Disconnect();
            });
        }

        public Task PublishAsync(string topic, string message)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(string topic, string message, int qos)
        {
            throw new NotImplementedException();
        }

        // -- properties

        public MqttClient Client { get { return this._client; } }

        public ConfigBase Config => _config;
    }
}
