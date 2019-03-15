using IOTConnect.Application.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    public class SensorDevicesModel
    {
        List<SensorDevice> senorDevices { get; set; }

        public SensorDevicesModel (List<SensorDevice> senorDevices)
        {
            this.senorDevices = senorDevices;
        }

        public List<SensorDevice> getAllSensors()
        {
            return senorDevices;
        }

        public void Add(SensorDevice sensor)
        {
            senorDevices.Add(sensor);
        }
    }
}
