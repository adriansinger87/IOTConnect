using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.IO;
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
        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new TestLogger());
            }
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
        }

        protected MqttConfig GetEnilinkMqttConfig(List<string> topics = null)
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"Test_Data\enilink-config.json");
            MqttConfig config = JsonIO.LoadFromJson<MqttConfig>(path);

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
