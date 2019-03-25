using IOTConnect.Application;
using IOTConnect.Application.Devices;
using IOTConnect.Application.Values;
using IOTConnect.Domain.Context;
using IOTConnect.Domain.IO;
using IOTConnect.Domain.Services.Mqtt;
using IOTConnect.Domain.System.Enumerations;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.IO;
using IOTConnect.Persistence.IO.Adapters;
using IOTConnect.Persistence.IO.Settings;
using IOTConnect.Services.Mqtt;
using IOTConnect.WebAPI.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace IOTConnect.WebAPI
{
    public class Startup
    {
        // -- fields

        private IHostingEnvironment _env;
        private IMqttControlable _mqtt;
        private IContextable _context;


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            //adds mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Used as memory storage for session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });

            services.AddSignalR();

            services.AddSingleton<IMqttControlable, MqttNetController>();

            services.AddSingleton<IContextable, DataContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMqttControlable mqtt, IContextable context)
        {
            _env = env;
            _mqtt = mqtt;
            _context = context;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseSession();
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSignalR(routes =>
            {
                routes.MapHub<DevicesHub>("/devicesHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            ConfigureMqtt();
        }

        private void ConfigureMqtt()
        {
            _mqtt.Connected += (o, e) =>
            {
                Log.Info($"Connected to {e.Broker} with id {e.ClientID}", Sources.Mqtt);
            };

            _mqtt.MessageReceived += (o, e) =>
            {
                Log.Trace($"{e.Topic}: {e.Message}", Sources.Mqtt);

                var context = _context as DataContext;

                if (e.Topic.StartsWith("LF/E"))
                {
                    // Enilink
                    var dev = context.Enilink.FirstOrNew(x => x.Id == e.Topic, out bool created);
                    if (created)
                    {
                        dev.Id = e.Topic;
                    }

                    var properties = e.Deserialize<List<EnilinkDevice>>(new EnilinkToValueStateAdapter());
                    dev.AppendProperties(properties);
                }
                else
                {
                    // M4.0 sensors
                    var dev = context.Sensors.FirstOrNew(x => x.Id == e.Topic, out bool created);
                    if (created)
                    {
                        dev.Id = e.Topic;
                    }
                    var value = e.Deserialize<ValueState>(new JsonToObjectAdapter());
                    dev.Data.Add(value);
                }
            };

            if (_mqtt.CreateClient(GetConfig()))
            {
                _mqtt.ConnectAsync();
            }
            else
            {
                Log.Error("Client was not created", Sources.Mqtt);
            }
        }

        private MqttConfig GetConfig()
        {
            IPersistenceControllable ioController = new IOController();

            var fs = new FileSettings
            {
                Location = Path.Combine(_env.ContentRootPath, "App_Data"),
                Name = "enilink-config.json"
            };

            var config = ioController.Importer(ImportTypes.Json)
                .Setup(fs)
                .Import()
                .ConvertWith<MqttConfig>(new JsonToObjectAdapter());

            return config;
        }

    }
}
