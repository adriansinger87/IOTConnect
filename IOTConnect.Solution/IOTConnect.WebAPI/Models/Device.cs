using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    public class Device
    {
    
        public string ID { get; set; }
        public string Name { get; set; }
        public double SensorValue { get; set; }

        public string Timestamp { get; set; }

        public DateTime DateAndTime { get { return DateTime.Parse(Timestamp); } }

    }
}
