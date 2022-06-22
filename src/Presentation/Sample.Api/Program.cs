using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sample.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel((context, op) =>
                {
                    op.Limits.MaxRequestBodySize = 1024 * 1024 * 2;//2MB
                    op.Limits.MinRequestBodyDataRate = null;
                    op.Limits.MaxConcurrentConnections = 100000;
                    op.Limits.MaxRequestBufferSize = null;
                    op.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(2);
                    op.Listen(
                        new IPEndPoint(
                            IPAddress.Parse(context.Configuration.GetValue<string>("Url:Ip")),
                            context.Configuration.GetValue<int>("Url:RestPort")), x => x.Protocols = HttpProtocols.Http1AndHttp2);
                })
                .UseShutdownTimeout(TimeSpan.FromSeconds(5))
                .ConfigureAppConfiguration(c =>
                {
                    c.Sources.Clear();
                    c.SetBasePath(Directory.GetCurrentDirectory());

#if DEBUG
                    c.AddJsonFile("appsettings.development.json", true, true);
#else
                    c.AddJsonFile("appsettings.json", true, true);
#endif

                    c.AddEnvironmentVariables();
                })
                .CaptureStartupErrors(true)

                .UseStartup<Startup>();
        }
    }
}
