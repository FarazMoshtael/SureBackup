
using Bogus;
using SureBackup.Domain.Entities;

namespace SureBackup.IntegrationTest.FakeBuilders;

public static class BackupSettingFaker
{
    public static Faker<BackupSetting> FekerRule()
    {
        return new Faker<BackupSetting>().RuleFor(item=>item.IntervalMiliseconds,prop=>prop.Random.Int(1500000,30000000))
            .RuleFor(item=>item.BackupOperationPath,prop=>prop.System.DirectoryPath())
            .RuleFor(item=>item.HostSizeInBytes,prop=>prop.Random.Int(100000000,500000000))
            .RuleFor(item=>item.EncryptedBackupKey,prop=>prop.Random.AlphaNumeric(50))
            .RuleFor(item=>item.FTPCredentialsAvailable,true).RuleFor(item=>item.FTPEncryptedUrl,prop=>prop.Internet.Url())
            .RuleFor(item=>item.FTPEncryptedUsername,prop=>prop.Random.AlphaNumeric(50))
            .RuleFor(item=>item.FTPUpload,true).RuleFor(item=>item.BackupKey,prop=>prop.Random.AlphaNumeric(10))
            .RuleFor(item=>item.EncryptedBackupKey,prop => prop.Random.AlphaNumeric(50))
            .RuleFor(item=>item.FTPUsername,prop=>prop.Name.Random.Word()).RuleFor(item=>item.EncryptionBackup,true)
            .RuleFor(item=>item.FTPUrl,prop=>prop.Random.AlphaNumeric(50))
            .RuleFor(item=>item.FTPPassword,prop=>prop.Random.AlphaNumeric(8)).RuleFor(item=>item.FTPEncryptedPassword, prop => prop.Random.AlphaNumeric(50));

    }
}
