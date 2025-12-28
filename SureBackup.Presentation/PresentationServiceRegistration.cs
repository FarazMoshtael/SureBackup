

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SureBackup.Domain.Common;
using SureBackup.Presentation.Abstraction;
using SureBackup.Presentation.Service;
using SureBackup.Presentation.UserControls;
using SureBackup.Presentation.ViewModels.UserControls;
using SureBackup.Presentation.ViewModels.Windows;
using SureBackup.Presentation.Windows;

namespace SureBackup.Presentation;

public static class PresentationServiceRegistration
{
    public static IHostBuilder RegisterPresentationServices(this IHostBuilder hostBuilder)
    {

        hostBuilder.ConfigureAppConfiguration((context, config) =>
        {
            // Add custom config files if needed
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        });
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<ISidebarNavigationService, SidebarNavigationService>();
            services.AddSingleton<IWindowNavigationService, WindowNavigationService>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeUserControl>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SettingUserControl>();
            services.AddSingleton<SettingViewModel>();
            services.AddSingleton<LogListUserControl>();
            services.AddSingleton<LogListViewModel>();
            services.AddSingleton<DatabaseListUserControl>();
            services.AddSingleton<DatabaseListViewModel>();
            services.AddSingleton<RestoreUserControl>();
            services.AddSingleton<RestoreViewModel>();
            services.AddTransient<MessageBoxWindow>();
            services.AddSingleton<MessageBoxViewModel>();
        });
        return hostBuilder;
    }
}
