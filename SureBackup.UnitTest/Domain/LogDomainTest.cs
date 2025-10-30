

using FluentAssertions;
using SureBackup.Domain.Entities;

namespace SureBackup.UnitTest.Domain;

public class LogDomainTest
{
    [Fact]
    public void LogEmptyMessage_ShouldThrowException()
    {
        //Arrange & Act
        Action act = () => new Log { Message = "" };
        //Assert
        act.Should().Throw<ArgumentException>();

    }
    [Fact]
    public void Log_ShouldNotThrowException()
    {
        //Arrange & Act
        Action act = () => new Log { Message = "Test log message" };
        //Assert
        act.Should().NotThrow();
    }
    [Fact]
    public void LogInitialization_ShouldSetPropertyValue()
    {
        Log log = new Log { Message = "Test log message" };
        log.Message.Should().NotBeEmpty();

    }
}
