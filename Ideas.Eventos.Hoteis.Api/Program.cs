﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ideas.Eventos.Hoteis.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();

            //CreateWebHostBuilder(args).Build().Run();
        }

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //        WebHost.CreateDefaultBuilder(args)
    //            .UseKestrel()
    //            .UseStartup<Startup>();
    }
}
