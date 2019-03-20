using IOTConnect.Application.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IOTConnect.Application.Devices
{
    public class EnilinkDevice : SensorDevice
    {
        public const string URI = "http://linkedfactory.iwu.fraunhofer.de/";

        public EnilinkDevice() : base()
        {
            Properties = new List<EnilinkDevice>();
        }

        // -- methods

        public bool HasProperties()
        {
            return (Properties.Count > 0 ? true : false);
        }

        public void AppendProperties(List<EnilinkDevice> properties)
        {
            foreach (EnilinkDevice prop in properties)
            {
                var found = Properties.FirstOrDefault(x => x.Id == prop.Id);

                if (found != null)
                {
                    found.Data.AddRange(found.Data.ToArray());
                    found.AppendProperties(prop.Properties);
                }
                else
                {
                    Properties.Add(found);
                }
            }
        }


        public override string ToString() => Id;

        // -- properties

        /// <summary>
        /// Gibt den Wert der ID-Eigenschaft aus ohne den Protokoll Prefix 'http://'
        /// </summary>
        public string Path { get { return Id.Replace("http://", ""); } }

        /// <summary>
        /// Gibt den Pfad bzw. die Http-Resource aus, die nach der Domain folgt.
        /// </summary>
        public string Resource { get { return Id.Replace(URI, ""); } }

        /// <summary>
        /// Gibt die Liste der Eigenschaften des Endpunktes aus oder legt diese fest.
        /// </summary>
        public List<EnilinkDevice> Properties { get; set; }

    }
}
