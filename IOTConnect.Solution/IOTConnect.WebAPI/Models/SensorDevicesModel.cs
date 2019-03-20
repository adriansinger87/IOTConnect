using IOTConnect.Application.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    /*
     * HACK @ AP: Wenn du eine Liste über eine Klasse verwalten möchtest, dann gibt es dazu bereits die Basisklasse "RepositoryBase" im domain layer.
     * Außerdem gibt es davon abgeleitet bereits eine Klasse DeviceRepository, die genau das macht. Diese findest du im application layer.
     * 
     * TODO @ AP: Nutze bitte das Konzept der anderen Layer im Projekt und implementiere dort Funktionen oder nutze Bestehendes.
     * Bei Fragen bitte einfach melden.
     */
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
