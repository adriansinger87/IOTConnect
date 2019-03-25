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

namespace IOTConnect.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        IMqttControlable _mqtt;
        private IHubContext<DevicesHub> _hubcontext;

        public HomeController(IMqttControlable mqtt, IHubContext<DevicesHub> hubcontext)
        {
            _mqtt = mqtt;
            _hubcontext = hubcontext;

            _mqtt.MessageReceived += (o, e) =>
            {
                //Log.Trace("");
                _hubcontext.Clients.All.SendAsync("MqttReceived", e.Topic, e.Message);
            }; 
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
