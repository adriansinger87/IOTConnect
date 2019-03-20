using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IOTConnect.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Inject(new NLogger());

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
