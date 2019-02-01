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

        }

        public override string ToString()
        {
            return $"{Id}: last value: {State.Value}";
        }

        // -- properties

        public string Id { get; set; }

        public ValueState State { get; set; }
    }
}
