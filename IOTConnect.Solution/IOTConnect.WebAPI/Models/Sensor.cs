using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    public class Sensor
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double SensorValue { get; set; }

        // TODO @ AP: warum kein DateTime oder string, der dann vom Datetime gecastet wird?
        public int Timestamp { get; set; }

        /*
         * TODO @ AP: Bitte die Klasse "aufräumen", auskommentierten "Zombi-" Code und meinen Kommentar entfernen.
         * 
         * Hinweis: Die Codebase für Properties kann in C# deutlich vereinfacht werden, wenn man keine Logik ausführt.
         * Dann lassen sichdeine 30 Zeilen als als 3-Zeiler angeben (siehe oben). Man kann also direkt mit den Properties arbeiten,
         * ohne eine private Variable zu benötigen.
         */

        //String iD;

        //public String ID
        //{
        //    get { return iD; }
        //    set { iD = value; }
        //}

        //String name;

        //public String Name
        //{
        //    get { return name; }
        //    set { name = value; }
        //}

        //double sensorValue;

        //public double SensorValue
        //{
        //    get { return sensorValue; }
        //    set { sensorValue = value; }
        //}

        //int timestamp;

        //public int Timestamp
        //{
        //    get { return timestamp; }
        //    set { timestamp = value; }
        //}
    }
}
