

using Bogus;
using SureBackup.Domain.Entities;

namespace SureBackup.UnitTest.FakeBuilders;

public static class DatabaseInfoFaker
{
    public static Faker<DatabaseInfo> FakerRule()
    {
        return new Faker<DatabaseInfo>().RuleFor(item => item.Database, prop => prop.PickRandom<SureBackup.Domain.Enums.Database>())
             .RuleFor(item => item.EncryptedConnectionString, prop => prop.Random.AlphaNumeric(250))
             .RuleFor(item => item.IsActive, property => property.Random.Bool())
             .RuleFor(item => item.Name, property => property.Name.Random.Word()).RuleFor(item=>item.ConnectionString,prop=>$"Server=localhost;Database=TestDatabase;");
    }

}
