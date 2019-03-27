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

        public DevicesController(IContextable context)
        {
            _context = context;
        }

        //TODO @ AS GET: api/devices isn't working anymore
        // GET: api/devices
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var list = _context.GetAllResources();
            return list;
        }

        // GET: api/devices/item?id=...
        [HttpGet("{id}", Name = "GetItem")]
        [Route("item")]
        public JsonResult Item([FromQuery(Name = "id")] string id)
        {
            var item = _context.GetResource(id, out bool found);
            return new JsonResult(item);
        }

        // GET: api/devices/data?id=...
        [HttpGet("{id}", Name = "GetData")]
        [Route("data")]
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
                    return x as ValueState;
                    // TODO @ AS create extension method ToViewModel() for this
                    //return new
                    //{
                    //    t = val.UtcTime,
                    //    v = val.Value
                    //};
                })
                .ToArray();

            return data;
        }
    }
}
