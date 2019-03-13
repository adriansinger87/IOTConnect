using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    [Obsolete]
    public class DeviceRegistration
    {
        List<Device> sensorList;
        static DeviceRegistration senreg = null;


        private DeviceRegistration ()
        {
            sensorList = new List<Device>();
        }

        /*
         * TODO @ AP: singleton pattern bitte verwerfen und durch eine nicht statische Instanz ersetzen. Diese z.B. in einer Session verwalten
         * Hintergrund: static fields werden von allen clients geteilt, die die Seite aufrufen, das führt bei Mehrfachzugriff auf die Seite zu ungewolltem Verhalten
         */
         /*
          * TODO @ AP: use DeviceBase, Circularbuffer from IOTConnect.Domain
          */
          //TODO @ AP: optional dd a session service to Startup.cs
        public static DeviceRegistration getInstance()
        {
            if (senreg == null)
            {
                senreg = new DeviceRegistration();
                return senreg;
            }

            else
            {
                return senreg;
            }
        }

        public void Add (Device sensor)
        {
            sensorList.Add(sensor);
        }

        public String Remove (String iD)
        {
            for (int i = 0; i < sensorList.Count; i++)
            {
                Device sensorN = sensorList.ElementAt(i);
                if(sensorN.ID.Equals(iD))
                {
                    sensorList.RemoveAt(i);
                    return "Delete succesfull";
                }
            }
            return "Delete un-succesfull";
        }

        public List<Device> getAllSensors()
        {
            return sensorList;
        }

        public String UpdateSensor (Device sen)
        {
            for (int i = 0; i < sensorList.Count; i++)
            {
                Device senN = sensorList.ElementAt(i);
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
