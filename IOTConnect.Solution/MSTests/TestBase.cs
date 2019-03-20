using IOTConnect.Domain.IO;
using IOTConnect.Domain.System.Enumerations;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.IO;
using IOTConnect.Persistence.IO.Adapters;
using IOTConnect.Persistence.IO.Settings;
using IOTConnect.Services.Mqtt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTests.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSTests
{
    public class TestBase
    {
        private IPersistenceControllable _io;

        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new TestLogger());
            }

            _io = new IOController();
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
        }

        protected MqttConfig GetEnilinkMqttConfig(List<string> topics = null)
        {
            var fs = new FileSettings
            {
                Location = Path.Combine(Environment.CurrentDirectory, @"Test_Data"),
                Name = "enilink-config.json"
            };

            var config = _io.Importer(ImportTypes.Json)
                .Setup(fs)
                .Import()
                .ConvertWith<MqttConfig>(new JsonToObjectAdapter());

            if (topics != null)
            {
                config.Topics.AddRange(topics.ToArray());
            }

            return config;
        }

        protected MqttConfig GetHiveMQConfig()
        {
            return new MqttConfig
            {
                ClientID = "IOTConnectDemo",
                Broker = "broker.hivemq.com",
                Port = 1883,
                Topics = new List<string> {
                "M40/HelloMqtt"
                }
            };
        }
    }
}
