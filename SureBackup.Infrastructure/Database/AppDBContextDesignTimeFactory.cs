

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SureBackup.Infrastructure.Database;

public class AppDBContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    public AppDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionsBuilder.UseSqlite($"Data Source={Environment.CurrentDirectory}\\SureBackup.db");

        return new AppDBContext(optionsBuilder.Options);
    }
}
