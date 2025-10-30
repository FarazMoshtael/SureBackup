

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.BackgroundService;
using SureBackup.Application.Service.BackupProcess;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.DBBackup;
using SureBackup.Application.Service.FTP;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Common;
using SureBackup.Infrastructure.Database;
using SureBackup.Infrastructure.Registration;
using SureBackup.Infrastructure.Repository;
using SureBackup.Infrastructure.Service.BackgroundService;
using SureBackup.Infrastructure.Service.BackupProcess;
using SureBackup.Infrastructure.Service.Cryption;
using SureBackup.Infrastructure.Service.DBBackup;
using SureBackup.Infrastructure.Service.FTP;
using SureBackup.Infrastructure.Service.Wrapper;

namespace SureBackup.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IHostBuilder RegisterInfrastructureServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.RegisterSerilog();
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddTransient<IBackupSettingRepository, BackupSettingRepository>();
            services.AddTransient<IDatabaseInfoRepository,DatabaseInfoRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddSingleton<ITextCryptionService, TextCryptionService>();
            services.AddSingleton<IStreamCryptionService, StreamCryptionService>();
            services.AddSingleton<IFTPClient, FTPClient>();
            services.AddSingleton<IFTPProcessService, FTPProcessService>();
            services.AddSingleton<IDirectoryWrapper,DirectoryWrapper>();
            services.AddSingleton<IFileWrapper, FileWrapper>();
            services.AddSingleton<IBackupRunnerService, BackupRunnerService>();
            services.AddSingleton<IIntervalBackgroundService, IntervalBackgroundService>();

            services.AddKeyedSingleton<IDBBackupService, SQLServerDBBackupService>(Constants.Service.SQLServerBackupService);
            services.AddKeyedSingleton<IDBBackupService, MySQLDBBackupService>(Constants.Service.MySQLBackupService);

            services.AddDbContext<AppDBContext>((builder) =>
            {
                builder.UseSqlite($"Data Source={Environment.CurrentDirectory}\\SureBackup.db");
            });

        });
        return hostBuilder;
    }
}
