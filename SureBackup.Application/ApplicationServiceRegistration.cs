

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace SureBackup.Application;

public static class ApplicationServiceRegistration
{
    public static IHostBuilder RegisterApplicationServices(this IHostBuilder hostBuilder) => hostBuilder.ConfigureServices((context, services) =>
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    });
}
