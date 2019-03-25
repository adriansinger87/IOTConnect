using System.Collections.Generic;
using System.Linq;
using IOTConnect.Application.Repository;
using IOTConnect.Domain.Models;

namespace IOTConnect.Application
{
    public class AppContainer : IAppContainer
    {
        public AppContainer()
        {
            Sensors = new SensorsRepository();
            Enilink = new EnilinkRepository();
        }

        public SensorsRepository Sensors { get; private set; }
        public EnilinkRepository Enilink { get; private set; }

        public List<string> GetAllDevices()
        {
            var list = new List<string>();

            list.AddRange(Sensors.Items.Select(s => s.Id).ToArray());
            list.AddRange(Enilink.Items.Select(s => s.Id).ToArray());

            return list;
        }
    }
}
