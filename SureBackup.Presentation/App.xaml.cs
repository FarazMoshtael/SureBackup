using Microsoft.Extensions.Hosting;
using System.Windows;
using SureBackup.Infrastructure;
using SureBackup.Application;
using SureBackup.Infrastructure.Registration;
using Microsoft.Extensions.DependencyInjection;
using SureBackup.Presentation.Windows;
namespace SureBackup.Presentation;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IHost? AppHost { get; private set; }
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        AppHost = Host.CreateDefaultBuilder().RegisterInfrastructureServices().RegisterApplicationServices().RegisterPresentationServices().Build();
        await AppHost.DBContextSeedAndMigration();

        MainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
    protected override async void OnExit(ExitEventArgs e)
    {
        if (AppHost is not null)
        {
            await AppHost.StopAsync();
            AppHost.Dispose();
        }
        base.OnExit(e);
    }
}

