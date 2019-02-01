using IOTConnect.Domain.Models.IoT;
using IOTConnect.Domain.Models.Values;
using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Domain.System.Extensions;
using IOTConnect.Persistence.IO;
using IOTConnect.Services.Mqtt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private List<DeviceBase> _devices;

        // -- overrides

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _isConnected = false;
            _devices = new List<DeviceBase>();

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
            var numMessages = 3;
            var watch = new Stopwatch();

            _mqtt.MessageReceived += (o, e) => {
                Log.Info($"{e.Topic}: {e.Message} after {watch.ElapsedMilliseconds} ms", Sources.Mqtt);
                messages.Add(e.Message);
            };

            // act
            await ConnectAsync();

            watch.Start();
            while (messages.Count < numMessages && watch.ElapsedMilliseconds < _duration) { }

            // assert
            Assert.IsTrue(messages.Count >= numMessages, $"topics shoud have sent {numMessages} messages");
        }

        [TestMethod]
        public async Task ReadTopicData()
        {
            // arrange
            var valueStates = new List<ValueState>();
            var numValues = 10;
            var numDevices = 2;
            var watch = new Stopwatch();

            _mqtt.MessageReceived += (o, e) => {

                var device = _devices.FirstOrNew(e.Topic, out bool created);
                if (created)
                {
                    _devices.Add(device);
                }

                var state = e.Deserialize<ValueState>(new ValueDeserializer());
                device.State = state;
                Log.Info($"{e.Topic}: data: {device.ToString()} after {watch.ElapsedMilliseconds} ms", Sources.Mqtt);
                valueStates.Add(state);
            };

            // act
            await ConnectAsync();

            watch.Start();
            while (valueStates.Count < numValues && watch.ElapsedMilliseconds < _duration) { }

            // assert
            Assert.IsTrue(_devices.Count >= numDevices, $"{numDevices} devices shoud have been received");
            Assert.IsTrue(valueStates.Count >= numValues, $"topics shoud have sent {numValues} messages");
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

            var mqtt = sender as MqttNetController;
            var config = mqtt.Config as MqttConfig;

            //_devices.AddRange(config.Topics.Select(s => new DeviceBase { Id = s }));
        }

    }
}
