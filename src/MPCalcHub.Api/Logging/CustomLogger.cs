using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPCalcHub.Api.Logging
{
    public class CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig) : ILogger
    {
        private readonly string loggerName = loggerName;
        private readonly CustomLoggerProviderConfiguration loggerConfig = loggerConfig;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"Log de execução: {logLevel} - {eventId.Id} - {formatter(state, exception)}";

            Console.WriteLine(message);      
        }
    }
}