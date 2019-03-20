using IOTConnect.Domain.Models.IoT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOTConnect.Domain.System.Extensions
{
    public static class DeviceListExtensions
    {
        //public static T FirstOrNew<T>(this List<T> list, string id, out bool created) where T : IDevice, new()
        //{
        //    var device = list.FirstOrDefault(d => d.Id == id);

        //    if (device != null)
        //    {
        //        created = false;
        //        Type deviceType = typeof(T);
        //        return (T)Convert.ChangeType(device, deviceType);
        //    }
        //    else
        //    {
        //        created = true;
        //        device = new T();
        //        device.Id = id;
        //        return device;
        //    }
        //}
    }
}
