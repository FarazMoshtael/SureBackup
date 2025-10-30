

using Bogus;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SureBackup.Application.Command.FTP;
using SureBackup.Application.Service.FTP;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;
using SureBackup.UnitTest.FakeBuilders;

namespace SureBackup.UnitTest.Usecase.Command;

public class FTPUploadCommandTest
{
    [Fact]
    public async Task UploadCommand_ShouldReturnSuccessResult()
    {
        //Arrange
        IFTPProcessService ftpProcessService=Substitute.For<IFTPProcessService>();
        ftpProcessService.CheckConnection().Returns(Result.Successful());
        ftpProcessService.ManageHostForRemainingStorage(currentBackupFilePath: Arg.Any<string>()).Returns(Result.Successful());
        ftpProcessService.UploadFile(filePath: Arg.Any<string>(), onUploadProgressUpdated: Arg.Any<Action<double>>()).Returns(Result.Successful());

        ILogger<FTPUploadCommandHandler> logger = Substitute.For<ILogger<FTPUploadCommandHandler>>();
        FTPUploadCommand command = new(new Faker().System.FilePath(), BackupSettingFaker.FekerRule().Generate(), (percentage) =>{});
        FTPUploadCommandHandler commandHandler=new(ftpProcessService, logger);

        //Act
        Result result=await commandHandler.Handle(command,CancellationToken.None);

        //Assert
        result.Success.Should().BeTrue();
    }
}
