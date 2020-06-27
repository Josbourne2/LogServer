using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogServer.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace LogServer.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            try
            {
                Log.Information("Starting up");
                Serilog.Debugging.SelfLog.Enable(msg => Log.Logger.Information(msg));
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration
                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
                        .WriteTo.Http("https://localhost:50001/api/logevents", httpClient: new CustomHttpClient(), textFormatter: new CustomTextFormatter())

                        .Enrich.FromLogContext()

                        );
                });
    }
}