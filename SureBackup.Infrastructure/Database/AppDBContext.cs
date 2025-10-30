
using Microsoft.EntityFrameworkCore;
using SureBackup.Domain.Entities;
using SureBackup.Infrastructure.EntityConfigurations;

namespace SureBackup.Infrastructure.Database;

public class AppDBContext:DbContext
{
    public AppDBContext()
    {
        
    }
    public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
    {
        
    }

    public DbSet<DatabaseInfo>? DatabaseInfoes { get; set; }
    public DbSet<BackupSetting>? BackupSettings { get; set; }
    public DbSet<Log>? Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BackupSettingEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DatabaseInfoEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LogEntityConfiguration());

    }

}
