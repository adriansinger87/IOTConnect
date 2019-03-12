using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.Logging;
using IOTConnect.Services.Mqtt;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IOTConnect.WebAPI
{
    

    public class Program
    {
        private IMqttControlable _mqtt;
        private MqttConfig _config;

        public static void Main(string[] args, IMqttControlable _mqtt, MqttConfig _config)
        {
            
            Log.Inject(new NLogger());

            // startup mqtt

                _mqtt = new MqttNetController();
                //_mqtt.Connected += OnConnected;
                _mqtt.CreateClient(_config);





            CreateWebHostBuilder(args).Build().Run();

            //public void OnConnected(object sender, MqttConnectedEventArgs e)
            //{
            //    Log.Info($"Connected to {e.Broker} with id {e.ClientID}", Sources.Mqtt);
            //    _isConnected = true;

            //    var mqtt = sender as MqttNetController;
            //    var config = mqtt.Config as MqttConfig;
            //}

        }
 

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


    }
}
