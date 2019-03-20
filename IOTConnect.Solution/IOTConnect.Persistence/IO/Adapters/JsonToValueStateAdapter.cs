using IOTConnect.Application.Values;
using IOTConnect.Domain.IO;
using IOTConnect.Domain.System.Logging;
using System;

namespace IOTConnect.Persistence.IO.Adapters
{
    public class JsonToValueStateAdapter : IAdaptable
    {
        public JsonToValueStateAdapter()
        {

        }

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
