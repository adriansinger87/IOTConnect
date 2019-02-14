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
