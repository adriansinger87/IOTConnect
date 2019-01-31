using IOTConnect.Domain.Models.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Models.IoT
{
    public class DeviceBase
    {
        public DeviceBase()
        {
            Childs = new List<DeviceBase>();
        }

        // -- properties

        public string Name { get; set; }

        public List<DeviceBase> Childs { get; set; }

        public ValueState State { get; set; }
    }
}
