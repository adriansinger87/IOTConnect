using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IOTConnect.WebAPI.Models;
using IOTConnect.Domain.Services.Mqtt;
using Microsoft.AspNetCore.SignalR;
using IOTConnect.WebAPI.Hubs;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Domain.Context;

namespace IOTConnect.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private IMqttControlable _mqtt;
        private IHubContext<DevicesHub> _hubcontext;
        private IContextable _context;

        public HomeController(IMqttControlable mqtt, IHubContext<DevicesHub> hubContext, IContextable context)
        {
            _mqtt = mqtt;
            _hubcontext = hubContext;
            _context = context;

            _mqtt.MessageReceived -= OnMqttEvent;
            _mqtt.MessageReceived += OnMqttEvent;
        }

        private void OnMqttEvent(object sender, MqttReceivedEventArgs e)
        {
            var device = _context.GetResource(e.Topic, out bool found);
            if (found)
            {
                _hubcontext.Clients.All.SendAsync("MqttReceived", e.Topic, device.LastData());
            }
           
        }

        public IActionResult Index()
        {
            SetViewBag();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetViewBag()
        {

            ViewBag.AppTitle = "IoT Connect";

            var version = typeof(Program).Assembly.GetName().Version.ToString();
#if DEBUG
            version += " beta";
#endif

            ViewBag.Version = version;
        }

     
    }
}
