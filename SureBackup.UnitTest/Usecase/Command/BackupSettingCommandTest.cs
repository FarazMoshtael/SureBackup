
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SureBackup.Application.Command.BackupOption;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.UnitTest.Usecase.Command;

public class BackupSettingCommandTest
{
    [Fact]
    public async Task SaveCommand_ShouldReturnSuccessResult()
    {
        //Arrange
        BackupSetting backupSetting = new BackupSetting()
        {
            IntervalMiliseconds = 10000,
            BackupOperationPath = new Faker().System.DirectoryPath(),
            HostSizeInBytes = 100000,
            FTPUpload = true,
            FTPEncryptedUrl = "ftp://fakeFTP.com",
            FTPEncryptedUsername = "FakeUser",
            FTPEncryptedPassword = "FakePass",
            EncryptedBackupKey = "FakeKey",
            EncryptionBackup = true
        };

        IBackupSettingRepository backupSettingRepository = Substitute.For<IBackupSettingRepository>();
        backupSettingRepository.SaveItemAsync(Arg.Any<BackupSetting>()).Returns(backupSetting);
        ITextCryptionService textCryptionService = Substitute.For<ITextCryptionService>();
        textCryptionService.Encrypt(Arg.Any<string>()).Returns("EnryptedText");
        ILogger<SaveBackupSettingCommandHandler> logger = Substitute.For<ILogger<SaveBackupSettingCommandHandler>>();
        IDirectoryWrapper directoryWrapper=Substitute.For<IDirectoryWrapper>();
        directoryWrapper.Exists(Arg.Any<string>()).Returns(true);
        SaveBackupSettingCommandHandler commandHandler = new(backupSettingRepository, textCryptionService, logger, directoryWrapper);
        SaveBackupSettingCommand command = new(IntervalMiliseconds: backupSetting.IntervalMiliseconds, BackupOperationPath: backupSetting.BackupOperationPath, HostSizeBytes: backupSetting.HostSizeInBytes,
         backupSetting.FTPUpload, "EncryptedURL", "EncryptedUser", "EncryptedPass", "EncryptedKey", true);
        //Act
        Result result = await commandHandler.Handle(command, CancellationToken.None);
        //Assert
        result.Success.Should().BeTrue();
    }
}
