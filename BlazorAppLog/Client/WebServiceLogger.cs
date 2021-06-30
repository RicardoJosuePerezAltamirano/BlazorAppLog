using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAppLog.Client
{
    public class WebServiceLogger:ILogger
    {
        protected readonly WebServiceLoggerProvider ServiceProvier;
        public WebServiceLogger(WebServiceLoggerProvider service)
        {
            ServiceProvier = service;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string Logdata = DateTime.Now.ToString();
            string dataException = exception != null ? exception.Message : state.ToString();
            BlazorAppLog.Shared.Log log = new BlazorAppLog.Shared.Log { Data = $"{Logdata} {dataException}", Level = logLevel.ToString() };
            string dataToSend = JsonSerializer.Serialize<BlazorAppLog.Shared.Log>(log);
            StringContent content = new StringContent(dataToSend, System.Text.Encoding.UTF8, "application/json");
            Task.Run(async delegate
            {
                var response = await ServiceProvier.Client.PostAsync("api/log/save", content);
                var IsOk = response.IsSuccessStatusCode;
                Console.WriteLine(IsOk+" status de la peticion");
            });
        }
    }
}
