using IOTConnect.Domain.IO;
using IOTConnect.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Persistence.IO.Adapters
{
    public class JsonAdapter : IAdaptable
    {
        public Tout Adapt<Tout, Tin>(Tin input) where Tout : new()
        {
            // type checks
            Tout output;
            Type inType = typeof(Tin);
  
            if (typeof(string).IsAssignableFrom(inType) == false)
            {
                Log.Error($"The JsonAdapter assumes a string as input type, but '{inType.Name}' was found.");
                return new Tout();
            }

            try
            {
                output = JsonIO.FromJsonString<Tout>(input as string);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not cast from '{typeof(Tin).Name}' to '{typeof(Tout).Name}' ");
                Log.Error(ex.Message);
                output = new Tout();
            }

            return output;
        }
    }
}
