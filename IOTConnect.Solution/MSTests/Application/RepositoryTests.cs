using IOTConnect.Application.Devices;
using IOTConnect.Application.Repository;
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
            var devicesRepo = new SensorsRepository();

            // act
            for (int i = 0; i < num; i++)
            {
                devicesRepo.Items.Add(new SensorDevice { Name = $"sensor {i + 1}" });
            }

            // assert
            Assert.IsTrue(devicesRepo.Count == num);
        }
    }
}
