using IOTConnect.Domain.Models.IoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOTConnect.Domain.System.Extensions
{
    public static class DeviceListExtensions
    {
        public static DeviceBase FirstOrNew(this List<DeviceBase> list, string id, out bool created)
        {
            var device = list.FirstOrDefault(d => d.Id == id);

            if (device != null)
            {
                created = false;
                return device;
            }
            else
            {
                created = true;
                return new DeviceBase { Id = id };
            }
        }
    }
}
