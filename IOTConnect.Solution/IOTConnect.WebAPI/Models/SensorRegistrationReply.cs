using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Models
{
    public class SensorRegistrationReply
    {
        String iD;

        public String ID
        {
            get { return iD; }
            set { iD = value; }
        }

        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        double sensorValue;

        public double SensorValue
        {
            get { return sensorValue; }
            set { sensorValue = value; }
        }

        String registrationStatus;

        public String RegistrationStatus
        {
            get { return registrationStatus; }
            set { registrationStatus = value; }
        }
    }
}
