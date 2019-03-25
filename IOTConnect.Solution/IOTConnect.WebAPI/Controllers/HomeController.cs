using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IOTConnect.WebAPI.Models;

namespace IOTConnect.WebAPI.Controllers
{
    public class HomeController : Controller
    {
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
