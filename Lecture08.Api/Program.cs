using Lecture08.Api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Lecture08.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

            hostBuilder.Seed();

            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
