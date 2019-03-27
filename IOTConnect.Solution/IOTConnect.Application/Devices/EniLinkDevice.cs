using IOTConnect.Application.Values;
using System.Collections.Generic;
using System.Linq;

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
                if (found == null)
                {
                    Properties.Add(prop);
                }
                else
                {
                    found.Data.AddRange(prop.Data.ToArray());
                }
            }
        }

        /// <summary>
        /// Returns the stored ValueState objects only from this instance or from the entire tree
        /// </summary>
        /// <param name="recursive">Defines if all Child nodes inside Properties are append their values to the returning list.
        /// Default value is false, so that no recursive call arises</param>
        /// <returns>returns a list of values</returns>
        public ValueState[] GetAllValues(bool recursive = false)
        {
            List<ValueState> values = base.Data.ToArray().ToList();
            if (recursive && Properties.Count > 0)
            {
                foreach (EnilinkDevice p in Properties)
                {
                    values.AddRange(p.GetAllValues(true));
                }
            }
            return values.ToArray();
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
