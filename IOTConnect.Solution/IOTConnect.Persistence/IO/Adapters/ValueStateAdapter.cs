using IOTConnect.Domain.IO;
using IOTConnect.Domain.Models.Values;
using IOTConnect.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Persistence.IO.Adapters
{
    public class ValueStateAdapter : IAdaptable
    {
        public Tout Adapt<Tout, Tin>(Tin input) where Tout : new()
        {
            // type checks
            Type outType = typeof(Tout);
            Type inType = typeof(Tin);

            if (typeof(ValueState).IsAssignableFrom(outType) == false ||
                typeof(string).IsAssignableFrom(inType) == false)
            {
                Log.Error($"The casting assumes the type '{typeof(ValueState).Name}' but '{outType.Name}' was found.");
                return new Tout();
            }

            // convert input, process and return output
            string inputString = input as string;
            Tout output = JsonIO.FromJsonString<Tout>(inputString);
            return output;
        }
    }
}
