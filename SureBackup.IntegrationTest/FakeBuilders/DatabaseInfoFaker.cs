

using Bogus;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;

namespace SureBackup.IntegrationTest.FakeBuilders;

public static class DatabaseInfoFaker
{
    public static Faker<DatabaseInfo> FakerRule()
    {
        return new Faker<DatabaseInfo>().RuleFor(item => item.Database, prop => prop.PickRandom<Domain.Enums.Database>())
             .RuleFor(item => item.EncryptedConnectionString, prop => prop.Random.AlphaNumeric(250))
             .RuleFor(item => item.IsActive, property => property.Random.Bool())
             .RuleFor(item => item.Name, property => property.Name.Random.Word()).RuleFor(item=>item.ConnectionString,prop=>$"Server=localhost;Database=TestDatabase;");
    }

    public static List<DatabaseInfo> Fake(bool useLog, int count)
    {
        var databaseInfoFaker = FakerRule();
        if (useLog)
        {
            var logFaker = LogFaker.FakerRule();

            databaseInfoFaker.RuleFor(item => item.BackupLogs, property => logFaker.Generate(property.Random.Int(1, 3)));
        }
        var databases = databaseInfoFaker.Generate(count);
        if (useLog)
            foreach (var database in databases)
            {
                foreach (var log in database.BackupLogs)
                    log.DatabaseInfo = database;
            }

        return databases;
    }
}
