using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    public class SensorRegistration
    {
        List<Sensor> sensorList;
        static SensorRegistration senreg = null;

        private SensorRegistration ()
        {
            sensorList = new List<Sensor>();
        }

        /*
         * TODO @ AP: singleton pattern bitte verwerfen und durch eine nicht statische Instanz ersetzen. Diese z.B. in einer Session verwalten
         * Hintergrund: static fields werden von allen clients geteilt, die die Seite aufrufen, das führt bei Mehrfachzugriff auf die Seite zu ungewolltem Verhalten
         */

        public static SensorRegistration getInstance()
        {
            if (senreg == null)
            {
                senreg = new SensorRegistration();
                return senreg;
            }

            else
            {
                return senreg;
            }
        }

        public void Add (Sensor sensor)
        {
            sensorList.Add(sensor);
        }

        public String Remove (String iD)
        {
            for (int i = 0; i < sensorList.Count; i++)
            {
                Sensor sensorN = sensorList.ElementAt(i);
                if(sensorN.ID.Equals(iD))
                {
                    sensorList.RemoveAt(i);
                    return "Delete succesfull";
                }
            }
            return "Delete un-succesfull";
        }

        public List<Sensor> getAllSensors()
        {
            return sensorList;
        }

        public String UpdateSensor (Sensor sen)
        {
            for (int i = 0; i < sensorList.Count; i++)
            {
                Sensor senN = sensorList.ElementAt(i);
                if(senN.ID.Equals(sen.ID))
                {
                    sensorList[i] = sen;
                    return "Update succesfull";
                }
            }
            return "Update un-succesfull";
        }
    }
}
