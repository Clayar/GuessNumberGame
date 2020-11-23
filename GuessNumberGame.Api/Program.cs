using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GuessNumberGame.Api
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

            var webHost = WebHost.CreateDefaultBuilder(args)
            .UseUrls($"http://localhost")
            .UseKestrel()
            .UseStartup<Startup>();

            return webHost;

        }
    }
}
