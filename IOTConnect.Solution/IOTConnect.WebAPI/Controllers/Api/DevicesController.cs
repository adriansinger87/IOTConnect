using IOTConnect.Application.Values;
using IOTConnect.Domain.Context;
using IOTConnect.Domain.Services.Mqtt;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IOTConnect.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        // -- fields

        private IContextable _context;

        // -- constructor

        public DevicesController(IMqttControlable mqtt, IContextable context)
        {
            _context = context;
        }

        // GET: api/devices
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var list = _context.GetAllRessources();
            return list;
        }

        // GET: api/devices/data?id=...
        [HttpGet("{id}", Name = "data")]
        public object[] Data([FromQuery(Name = "id")] string id)
        {
            /*
             * .GetData()      -> fetch object array from _context by id
             * .AsParallel()   -> parallelize following operations with 
             * .Select()       -> change oject type from ValueState to a dynamic type
             * .ToArray()      -> convert to an array
             */
            object[] data = _context
                .GetData(id)
                .AsParallel()
                .Select((x) =>
                {
                    var val = x as ValueState;
                    return new
                    {
                        t = val.UtcTime,
                        v = val.Value
                    };
                })
                .ToArray();

            return data;
        }
    }
}
