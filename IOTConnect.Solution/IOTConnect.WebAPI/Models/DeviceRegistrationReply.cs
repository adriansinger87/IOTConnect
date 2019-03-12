using System;

namespace IOTConnect.WebAPI.Models
{
    [Obsolete]
    public class DeviceRegistrationReply
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

        int timestamp;

        public int Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        String registrationStatus;

        public String RegistrationStatus
        {
            get { return registrationStatus; }
            set { registrationStatus = value; }
        }
    }
}
