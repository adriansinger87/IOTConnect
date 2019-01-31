using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.IO;
using IOTConnect.Services.Mqtt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MSTests.Services.Mqtt
{
    [TestClass]
    public class MqttNetTests : TestBase
    {
        // -- fields

        private bool _isConnected;
        private MqttConfig _config;
        private IMqttControlable _mqtt;

        private int _duration = 30000;

        // -- overrides

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _isConnected = false;

            _config = base.GetEnilinkMqttConfig();
            //_config = base.GetHiveMQConfig();

            _mqtt = new MqttNetController();
            _mqtt.Connected += OnConnected;
            _mqtt.CreateClient(_config);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- test methods

        [TestMethod]
        public async Task ConnectMqttNet()
        {
            // arrange & act
            await ConnectAsync();

            // assert
            Assert.IsTrue(_isConnected);
        }

        [TestMethod]
        public async Task ReadTopicRaw()
        {
            // arrange
            var messages = new List<string>();
            var numMessages = 2.5;
            var watch = new Stopwatch();

            _mqtt.MessageReceived += (o, e) => {
                messages.Add(e.Message);
                Log.Info($"{e.Topic}: {e.Message} after {watch.ElapsedMilliseconds} ms", Sources.Mqtt);
            };

            // act
            await ConnectAsync();

            watch.Start();
            while (messages.Count < numMessages && watch.ElapsedMilliseconds < _duration)
            {
               
            }

            // assert
            Assert.IsTrue(messages.Count >= numMessages, $"topics shoud have sent {numMessages} messages");
        }

        // -- private methods

        private async Task ConnectAsync()
        {
            Log.Info($"Connecting with {_config.ToString()}", Sources.Mqtt);
            await _mqtt.ConnectAsync();
        }

        private void OnConnected(object sender, MqttConnectedEventArgs e)
        {
            Log.Info($"Connected to {e.Broker} with id {e.ClientID}", Sources.Mqtt);
            _isConnected = true;
        }

    }
}
