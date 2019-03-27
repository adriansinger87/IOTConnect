using IOTConnect.Application.Repository;
using IOTConnect.Domain.Context;
using IOTConnect.Domain.Models.IoT;
using System.Collections.Generic;
using System.Linq;

namespace IOTConnect.Application
{
    public class DataContext : IContextable
    {

        // -- constructor

        public DataContext()
        {
            Sensors = new SensorsRepository();
            Enilink = new EnilinkRepository();
        }

        // -- methods

        public List<string> GetAllResources()
        {
            var list = new List<string>();

            list.AddRange(Sensors.Items.Select(s => s.Id).ToArray());
            list.AddRange(Enilink.Items.Select(s => s.Id).ToArray());

            return list;
        }

        public object[] GetData(string id)
        {
            IDevice dev = GetResource(id, out bool found);

            if (found)
            {
                return dev.GetData();
            }
            else
            {
                return new object[] { };
            }
        }

        public IDevice GetResource(string id, out bool found)
        {
            DeviceBase item = null;
            found = false;

            // select item by id
            if (Sensors.Items.Any(x => x.Id == id))
            {
                item = Sensors.Items.First(x => x.Id == id);
            }
            else if (Enilink.Items.Any(x => x.Id == id))
            {
                item = Enilink.Items.First(x => x.Id == id);
            }

            // result
            if (item != null)
            {
                found = true;
            }
            return item;
        }

        // -- properties

        public SensorsRepository Sensors { get; private set; }
        public EnilinkRepository Enilink { get; private set; }

    }
}
