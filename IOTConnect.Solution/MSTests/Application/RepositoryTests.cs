using IOTConnect.Application;
using IOTConnect.Application.Devices;
using IOTConnect.Application.Repository;
using IOTConnect.Domain.Models.IoT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests.Application
{
    [TestClass]
    public class RepositoryTests : TestBase
    {
        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- Test methods

        [TestMethod]
        public void FillDevicesRepository()
        {
            // arrange
            var num = 10;
            var sensors = new SensorsRepository();

            // act
            FillSensors(sensors, 10);

            // assert
            Assert.IsTrue(sensors.Count == num);
        }

        private SensorsRepository FillSensors(SensorsRepository sensors, int num = 100)
        {
            if (sensors == null)
            {
                sensors = new SensorsRepository();
            }

            for (int i = 0; i < num; i++)
            {
                sensors.Items.Add(new SensorDevice { Id = $"sensor {i + 1}", Name = $"sensor name {i + 1}" });
            }

            return sensors;
        }

        [TestMethod]
        public void GetResourceData()
        {
            // arrange
            var id = "sensor 5";
            var context = new DataContext();
            FillSensors(context.Sensors);

            // act
            SensorDevice dev = context.GetResource(id, out bool found) as SensorDevice;

            // assert
            Assert.IsTrue(found, $"'{id}' not found");

        }
    }
}
