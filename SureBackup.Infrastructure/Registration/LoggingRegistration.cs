
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SureBackup.Domain.Common;

namespace SureBackup.Infrastructure.Registration;

public static class LoggingRegistration
{
    public static IHostBuilder RegisterSerilog(this IHostBuilder hostBuilder)
    {
 
        hostBuilder.UseSerilog((context, services, config) =>
        {
            string? logPath = context.Configuration[ConfigurationKey.Logging.LogPath];
            if (!string.IsNullOrWhiteSpace(logPath))
                config.MinimumLevel.Debug().Enrich.FromLogContext()
                         .WriteTo.File($"{logPath}\\Information.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
                        .WriteTo.File($"{logPath}\\Error.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error);
        });
        return hostBuilder;
    }
}
