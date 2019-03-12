using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    // TODO @ AP remove obsolete classes ans use implementations from application layer
    [Obsolete]
    public class Device
    {
    
        public string ID { get; set; }
        public string Name { get; set; }
        public double SensorValue { get; set; }

        public string Timestamp { get; set; }

        // TODO @ AP: die Benennung einer Property sollte auch einen Wert widerspiegeln und nicht zwei
        // zur Vermeidung der Wiederholung des Klassennamens beim Typ DateTime wird häufig "Timestamp" als Name der Eigenschaft genommen 
        public DateTime DateAndTime { get { return DateTime.Parse(Timestamp); } }

    }
}
