using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorAppLog.Client
{
    public class Program
    {
        private static WebServiceLoggerProvider LoggerProvider;
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var Client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            builder.Logging.SetMinimumLevel(LogLevel.Information);

            LoggerProvider = new WebServiceLoggerProvider(Client);

            builder.Logging.AddProvider(LoggerProvider);


            builder.RootComponents.Add<App>("app");

            
            builder.Services.AddTransient(sp =>Client);

            await builder.Build().RunAsync();
        }
    }
}
