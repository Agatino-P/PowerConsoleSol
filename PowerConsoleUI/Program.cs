using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace PowerConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigurationBuilder cfgBuilder = new();
            buildConfig(cfgBuilder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(cfgBuilder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                    {
                        services.AddTransient<IGreetingService, GreetingService>();
                        //services.AddSingleton<Serilog.ILogger>(Log.Logger);
                        //services.AddSingleton<IConfiguration>(Log.Logger);
                    })
                .UseSerilog()
                .Build();

            var greetingService = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
            greetingService.Run();


        }

        private static void buildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"asspesttings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}",
                    optional: true)
                .AddEnvironmentVariables();

        }
    }
}
