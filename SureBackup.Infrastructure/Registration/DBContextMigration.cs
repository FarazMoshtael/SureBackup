

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SureBackup.Domain.Entities;
using SureBackup.Infrastructure.Database;

namespace SureBackup.Infrastructure.Registration;

public static class DBContextMigration
{
    public static async Task<IHost> DBContextSeedAndMigration(this IHost host)
    {
        AppDBContext Context = host.Services.GetRequiredService<AppDBContext>();
        Context.Database.Migrate();

        if (Context.BackupSettings?.Any() == false)
        {
            BackupSetting defaultSetting = new BackupSetting
            {
                BackupOperationPath = $"{Environment.CurrentDirectory}\\BackupOperation"
            };
            if(!Directory.Exists(defaultSetting.BackupOperationPath))
                Directory.CreateDirectory(defaultSetting.BackupOperationPath);
            Context.BackupSettings.Add(defaultSetting);
            await Context.SaveChangesAsync();
        }

        return host;
    }
}
