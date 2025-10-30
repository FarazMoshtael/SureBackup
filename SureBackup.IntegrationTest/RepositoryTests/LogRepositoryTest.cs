
using FluentAssertions;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;
using SureBackup.Infrastructure.Database;
using SureBackup.Infrastructure.Repository;
using SureBackup.IntegrationTest.FakeBuilders;
using SureBackup.IntegrationTest.Fixtures;

namespace SureBackup.IntegrationTest.RepositoryTests;

public class LogRepositoryTest
{

    [Fact]
    public async Task NewErrorLogAsync_ShouldReturnSuccessResult()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture().DatabaseContext;

        var logRepository = new LogRepository(Context);
        var database = DatabaseInfoFaker.FakerRule().Generate();

        //Act
        Result result = await logRepository.NewErrorLogAsync("test", database);
        Context.ChangeTracker.Clear();

        //Assert
        result.Success.Should().BeTrue();
    }
    [Fact]
    public async Task NewInformationLogAsync_ShouldReturnSuccessResult()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture().DatabaseContext;
        var logRepository = new LogRepository(Context);
        var database = DatabaseInfoFaker.FakerRule().Generate();

        //Act
        Result result = await logRepository.NewInformationLogAsync("test", database);
        Context.ChangeTracker.Clear();

        //Assert
        result.Success.Should().BeTrue();
    }

}
