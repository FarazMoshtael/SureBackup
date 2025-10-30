
using Bogus;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SureBackup.Application.Command.DBBackup;
using SureBackup.Application.Service.DBBackup;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;
using SureBackup.UnitTest.FakeBuilders;

namespace SureBackup.UnitTest.Usecase.Command;

public class DatabaseBackupCommandTest
{
    [Fact]
    public async Task ExportBackup_ShouldReturnSuccessResult()
    {
        //Arrange
        IDBBackupService sqlServerDBBackupService=Substitute.For<IDBBackupService>();
        sqlServerDBBackupService.BackupDatabase(databaseInfo: Arg.Any<DatabaseInfo>(), destinationPath: Arg.Any<string>())
            .Returns(Result<string>.Successful("Sample SQL Server Path"));

        IDBBackupService mysqlDBBackupService = Substitute.For<IDBBackupService>();
        mysqlDBBackupService.BackupDatabase(databaseInfo: Arg.Any<DatabaseInfo>(),destinationPath:Arg.Any<string>())
            .Returns(Result<string>.Successful("Sample MySQL Path"));

        ILogger<DBBackupCommandHandler> logger = Substitute.For<ILogger<DBBackupCommandHandler>>();
        IDirectoryWrapper directoryWrapper = Substitute.For<IDirectoryWrapper>();
        directoryWrapper.Exists(Arg.Any<string>()).Returns(true);
        DBBackupCommand command = new DBBackupCommand(DatabaseInfoFaker.FakerRule().Generate(),BackupSettingFaker.FekerRule().Generate());
        DBBackupCommandHandler commandHandler = new(sqlServerbackupService:sqlServerDBBackupService,mysqlBackupService:mysqlDBBackupService,logger:logger,directoryWrapper: directoryWrapper);
        //Act
        Result<string> result = await commandHandler.Handle(command,CancellationToken.None);

        //Assert
         result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }
}
