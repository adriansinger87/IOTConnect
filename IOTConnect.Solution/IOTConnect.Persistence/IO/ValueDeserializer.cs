using IOTConnect.Domain.IO;
using IOTConnect.Domain.Models.Values;
using IOTConnect.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Persistence.IO
{
    public class ValueDeserializer : IDeserializable
    {
        public T Deserialize<T>(object data) where T : new()
        {
            Type outType = typeof(T);

            if (typeof(ValueState).IsAssignableFrom(outType) == false)
            {
                Log.Error($"The casting assumes the type '{typeof(ValueState).Name}' but '{outType.Name}' was found.");
                return new T();
            }

            T output = JsonIO.FromJsonString<T>(data.ToString());

            return output;
        }
    }
}
