using IOTConnect.Application.Repository;
using System.Collections.Generic;

namespace IOTConnect.Application
{
    public interface IAppContainer
    {
        List<string> GetAllDevices();

        SensorsRepository Sensors { get; }
        EnilinkRepository Enilink { get; }
    }
}
