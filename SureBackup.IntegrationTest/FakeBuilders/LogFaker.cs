
using Bogus;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Enums;

namespace SureBackup.IntegrationTest.FakeBuilders;

public static class LogFaker
{
    public static Faker<Log> FakerRule()
    {
        return new Faker<Log>().RuleFor(item => item.Type, prop => prop.PickRandom<AppLogType>())
                .RuleFor(item => item.Date, prop => prop.Date.Past())
                .RuleFor(item => item.Message, prop => prop.Random.Words());
    }
    public static List<Log> Fake(int count)
    {
        var logFaker = FakerRule();
        logFaker.RuleFor(item => item.DatabaseInfo, property => DatabaseInfoFaker.FakerRule().Generate());
        
        return logFaker.Generate(count);
    }
}
