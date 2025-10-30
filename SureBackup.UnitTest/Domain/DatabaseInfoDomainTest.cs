

using FluentAssertions;
using SureBackup.Domain.Entities;

namespace SureBackup.UnitTest.Domain;

public class DatabaseInfoDomainTest
{
    [Fact]
    public void DatabaseInfoEmptyName_ShouldThrowException()
    {
        //Arrange & Act
        Action act = () => new DatabaseInfo { EncryptedConnectionString = "test", Name = "" };
        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DatabaseInfoEmptyConnectionString_ShouldThrowException()
    {
        //Arrange & Act
        Action act = () => new DatabaseInfo { EncryptedConnectionString = "", Name = "test" };
        //Assert
        act.Should().Throw<ArgumentException>();
    }
    [Fact]
    public void DatabaseInfoInitialization_ShouldSetPropertyValue() {

        //Arrange & Act
        DatabaseInfo database= new DatabaseInfo { EncryptedConnectionString = "test", Name = "test" };
        //Assert
        database.EncryptedConnectionString.Should().NotBeEmpty();
        database.Name.Should().NotBeEmpty();


    }
}
