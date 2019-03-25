using System;
using System.Collections.Generic;
using System.Linq;
using IOTConnect.Application.Repository;
using IOTConnect.Domain.Context;
using IOTConnect.Domain.Models;

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

        public List<string> GetAllRessources()
        {
            var list = new List<string>();

            list.AddRange(Sensors.Items.Select(s => s.Id).ToArray());
            list.AddRange(Enilink.Items.Select(s => s.Id).ToArray());

            return list;
        }

        public object[] GetData(string id)
        {
            if (Sensors.Items.Any(x => x.Id == id))
            {
                return Sensors.Items.First(x => x.Id == id).Data.ToArray();
            }
            else if (Enilink.Items.Any(x => x.Id == id))
            {
                return Enilink.Items.First(x => x.Id == id).Data.ToArray();
            }
            else
            {
                return new object[] { };
            }
        }
        
        // -- properties

        public SensorsRepository Sensors { get; private set; }
        public EnilinkRepository Enilink { get; private set; }

    }
}
