

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using SureBackup.Application.Service.Cryption;
using SureBackup.Domain.Entities;
using SureBackup.Infrastructure.Database;
using SureBackup.Infrastructure.Repository;
using SureBackup.IntegrationTest.FakeBuilders;
using SureBackup.IntegrationTest.Fixtures;

namespace SureBackup.IntegrationTest.RepositoryTests;
public class BackupSettingRepositoryTest
{

    [Fact]
    public async Task GetAvailableItem_ShouldReturnEntityByDecryptedProperties()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture(seedBackupSetting: true).DatabaseContext;
        BackupSetting backupSetting =(await Context.BackupSettings!.FirstOrDefaultAsync())!;
        ITextCryptionService textCryptionService=Substitute.For<ITextCryptionService>();
        textCryptionService.Decrypt(cipherText: Arg.Is(backupSetting.EncryptedBackupKey!)).Returns(backupSetting.BackupKey);
        textCryptionService.Decrypt(cipherText: Arg.Is(backupSetting.FTPEncryptedUrl!)).Returns(backupSetting.FTPUrl);
        textCryptionService.Decrypt(cipherText: Arg.Is(backupSetting.FTPEncryptedUsername!)).Returns(backupSetting.FTPUsername);
        textCryptionService.Decrypt(cipherText: Arg.Is( backupSetting.FTPEncryptedPassword!)).Returns(backupSetting.FTPPassword);
        BackupSettingRepository backupSettingRepository = new (textCryptionService,Context);
        Context.ChangeTracker.Clear();

        //Act
        BackupSetting availableSetting = backupSettingRepository.GetAvailableBackupSetting();

        //Assert
        availableSetting.FTPUrl.Should().Be(backupSetting.FTPUrl);
        availableSetting.FTPUsername.Should().Be(backupSetting.FTPUsername);
        availableSetting.FTPPassword.Should().Be(backupSetting.FTPPassword);
        availableSetting.BackupKey.Should().Be(backupSetting.BackupKey);


    }


}
