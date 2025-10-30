
using Bogus;
using FluentAssertions;
using SureBackup.Domain.Entities;

namespace SureBackup.UnitTest.Domain;

public class BackupSettingDomainTest
{
    [Fact]
    public void BackupSettingInvalidInterval_ShouldThrowException()
    {
        //Arrange & Act
        Action act = () => new BackupSetting
        {
            IntervalMiliseconds = 0,
            HostSizeInBytes = 0,
            BackupOperationPath = new Faker().System.DirectoryPath()
        };
        //Assert
        act.Should().Throw<ArgumentException>();
    }


    [Fact]
    public void BackupSettingInvalidBackupOperationPath_ShouldThrowException()
    {
        Action act = () => new BackupSetting
        {
            IntervalMiliseconds = 5,
            HostSizeInBytes = 0,
            BackupOperationPath = "/"
        };
        act.Should().Throw<System.ComponentModel.DataAnnotations.ValidationException>();
    }

}
