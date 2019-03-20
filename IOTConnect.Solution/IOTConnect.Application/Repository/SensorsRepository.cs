using IOTConnect.Application.Devices;
using IOTConnect.Domain.Models.Repository;

namespace IOTConnect.Application.Repository
{
    public class SensorsRepository : RepositoryBase<SensorDevice>
    {
        public SensorsRepository()
        {
            Name = "sensors repository";
        }
    }
}
