using IOTConnect.Application.Devices;
using IOTConnect.Domain.Models.Repository;

namespace IOTConnect.Application.Repository
{
    public class EnilinkRepository : RepositoryBase<EnilinkDevice>
    {
        public EnilinkRepository()
        {
            Name = "enilink repository";
        }

    }
}
