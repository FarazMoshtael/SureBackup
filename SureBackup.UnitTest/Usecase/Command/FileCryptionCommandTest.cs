

using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SureBackup.Application.Command.FileCryption;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Pattern;
using SureBackup.UnitTest.FakeBuilders;
using System.Text;

namespace SureBackup.UnitTest.Usecase.Command;

public class FileCryptionCommandTest
{
    [Fact]
    public async Task EncryptionCommand_ShouldReturnSuccessResultByEncryptedPath()
    {
        //Arrange
        IStreamCryptionService streamCryptionService = Substitute.For<IStreamCryptionService>();
        ILogger<FileEncryptionCommandHandler> logger = Substitute.For<ILogger<FileEncryptionCommandHandler>>();
        IFileWrapper fileWrapper = Substitute.For<IFileWrapper>();
        var sourceContentBytes = Encoding.UTF8.GetBytes("Sample Content");
        using var sourceStream = new MemoryStream(sourceContentBytes);
        using var destinationStream = new MemoryStream();
        fileWrapper.OpenReadFile(path: Arg.Any<string>()).Returns(sourceStream);
        fileWrapper.CreateFile(path: Arg.Any<string>()).Returns(destinationStream);
        FileEnryptionCommand command = new(new Faker().System.FilePath(), BackupSettingFaker.FekerRule().Generate());
        FileEncryptionCommandHandler commandHandler = new(streamCryptionService, logger, fileWrapper);
        //Act
        Result<string> result = await commandHandler.Handle(command, CancellationToken.None);
        //Assert
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
    }

}
