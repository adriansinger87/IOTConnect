using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using IOTConnect.Domain.Context;
using IOTConnect.Domain.Services.Mqtt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOTConnect.WebAPI.Controllers.Api
{
    [Route("api/v2/devices")]
    [ApiController]
    public class DevicesV2Controller : ControllerBase
    {
        // -- fields

        private IContextable _context;

        // -- constructor

        public DevicesV2Controller(IContextable context)
        {
            _context = context;
        }

        // GET: api/devices/item?id=...
        [HttpGet]
        [Route("item")]
        public JsonResult Item([FromQuery(Name = "id")] string id, [FromQuery(Name = "limit")] int limit)
        {
            var item = _context.GetResource(id, out bool found);

            var viewModel = new
            {
                id = item.Id,
                name = item.Name,
                data = item.GetData().Reverse().Take(limit)
            };

            return new JsonResult(viewModel);
        }
    }
}