using IOTConnect.Application.Devices;
using IOTConnect.Domain.Models.Repository;

namespace IOTConnect.Application.Repository
{
    public class DevicesRepository : RepositoryBase<SensorDevice>
    {
        public DevicesRepository()
        {
            Name = "devices repository";
        }
    }
}
