using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Logging;
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
    public class MqttNetTests : BaseTests
    {

        private bool _isConnected;
        private MqttConfig _config;
        private IMqttControlable _mqtt;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _isConnected = false;

            _config = new MqttConfig
            {
                ClientID = "IOTConnectDemo",
                Broker = "linkedfactory.iwu.fraunhofer.de",
                Port = 8883,
                Topics = new List<string> {
                "LF/E/linkedfactory.iwu.fraunhofer.de/linkedfactory/IWU/FoFab/SolarPlant"
                }
            };

            _mqtt = new MqttNetController();
            _mqtt.Connected += IsConnected;
            _mqtt.CreateClient(_config);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task ReadTopics()
        {
            // arrange
            var messages = new List<string>();
            var counter = 2;
            var ms = 30000;
            var watch = new Stopwatch();

            _mqtt.MessageReceived += (o, e) => {
                messages.Add(e.Message);
                Log.Info($"{e.Topic}: {e.Message} after {watch.ElapsedMilliseconds} ms", Sources.Mqtt);
            };

            // act
            await ConnectAsync();

            watch.Start();
            while (messages.Count < counter && watch.ElapsedMilliseconds < ms)
            {
               
            }

            // assert
            Assert.IsTrue(messages.Count >= counter, $"topics shoud have sent {counter} messages");
        }

        [TestMethod]
        public async Task ConnectMqttNet()
        {
            // arrange & act
            await ConnectAsync();

            // assert
            Assert.IsTrue(_isConnected);
        }

        private async Task ConnectAsync()
        {
            Log.Info($"Connecting with {_config.ToString()}", Sources.Mqtt);
            await _mqtt.ConnectAsync();
        }

        private void IsConnected(object sender, MqttConnectedEventArgs e)
        {
            Log.Info($"Connected to {e.Broker} with id {e.ClientID}", Sources.Mqtt);
            _isConnected = true;
        }

    }
}
