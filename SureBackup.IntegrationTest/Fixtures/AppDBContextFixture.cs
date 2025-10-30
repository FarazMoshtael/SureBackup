
using Microsoft.EntityFrameworkCore;
using SureBackup.Infrastructure.Database;
using SureBackup.IntegrationTest.FakeBuilders;

namespace SureBackup.IntegrationTest.Fixtures;

public class AppDBContextFixture
{
    public AppDBContextFixture(bool seedDatabase = false,bool seedBackupSetting=false)
    {
        DatabaseContext = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
        if (seedDatabase)
            SeedDatabase();
        if (seedBackupSetting)
            SeedBackupSetting();
    }
    public AppDBContext DatabaseContext { get; set; }
    private void SeedDatabase()
    {
        DatabaseContext.DatabaseInfoes!.AddRange(DatabaseInfoFaker.Fake(false, 5));
        DatabaseContext.SaveChanges();
    }

    private void SeedBackupSetting()
    {
        DatabaseContext.BackupSettings!.Add(BackupSettingFaker.FekerRule().Generate());
        DatabaseContext.SaveChanges();
    }
}
