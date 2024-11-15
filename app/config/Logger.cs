using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace log_storage.app.config
{
    public class Logger
    {
        private readonly ILogger<Logger> _logger;
    
        public Logger()
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) 
               .CreateLogger();

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog(); // Integrando Serilog com o sistema de logging do Dotnet
                })
                .BuildServiceProvider();

            _logger = serviceProvider.GetService<ILogger<Logger>>();

            Log.Information("create log instance with sucessfully");
        }

        public ILogger<Logger> getInstance()
        {
            return _logger;
        }

    }
}
