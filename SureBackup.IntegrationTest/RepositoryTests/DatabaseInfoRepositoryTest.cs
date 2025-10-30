
using Bogus;
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
public class DatabaseInfoRepositoryTest
{
    private DatabaseInfoRepository GetRepository(AppDBContext Context)
    {
        ITextCryptionService textCryptionService = Substitute.For<ITextCryptionService>();
        DatabaseInfoRepository databaseInfoRepository = new(Context, textCryptionService);
        return databaseInfoRepository;
    }
    [Fact]
    public async Task AddAsync_ShouldSaveEnityToDatabase()
    {

        //Arrange
        using AppDBContext Context=new AppDBContextFixture().DatabaseContext;
        DatabaseInfo databaseInfo = DatabaseInfoFaker.FakerRule().Generate();
        DatabaseInfoRepository databaseInfoRepository = GetRepository(Context);

        //Act
        await databaseInfoRepository.AddAsync(databaseInfo);
        Context.ChangeTracker.Clear();

        //Assert
        DatabaseInfo? savedEntity = await Context.DatabaseInfoes!.FindAsync(databaseInfo.ID);
        savedEntity.Should().NotBeNull();
        savedEntity.ID.Should().Be(databaseInfo.ID);
        savedEntity.Name.Should().Be(databaseInfo.Name);

    }

    [Fact]
    public async Task AddRangeAsync_ShouldSaveRangeEntities()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture().DatabaseContext;

        List<DatabaseInfo> databases = DatabaseInfoFaker.Fake(false, 5);
        DatabaseInfoRepository databaseInfoRepository = GetRepository(Context);

        //Act
        await databaseInfoRepository.AddRangeAsync(databases);
        Context.ChangeTracker.Clear();

        //Assert
        int[] addedDatabaseIDs = databases.Select(x => x.ID).ToArray();
        List<DatabaseInfo> savedEntities = await Context.DatabaseInfoes!.Where(item => addedDatabaseIDs.Contains(item.ID)).ToListAsync();
        foreach (var entity in databases) {
            DatabaseInfo? generatedDBEntity = savedEntities.Find(item => item.ID == entity.ID);
            generatedDBEntity.Should().NotBeNull();
            generatedDBEntity.Name.Should().Be(entity.Name);
        }
    }
    [Fact]
    public async Task UpdateAsync_ShouldModifyDBEntityProperty()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture(seedDatabase:true).DatabaseContext;

        DatabaseInfoRepository databaseInfoRepository = GetRepository(Context);
        DatabaseInfo? entity =await Context.DatabaseInfoes!.FirstOrDefaultAsync();
        entity!.Name = new Faker().Name.Random.Word();

        //Act
        await databaseInfoRepository.UpdateAsync(entity);
        Context.ChangeTracker.Clear();

        //Assert
        DatabaseInfo? modifiedEntity = await Context.DatabaseInfoes!.FindAsync(entity.ID);
        modifiedEntity.Should().NotBeNull();
        modifiedEntity.Name.Should().Be(entity.Name);

    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteDBEntity()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture(seedDatabase:true).DatabaseContext;
        DatabaseInfoRepository databaseInfoRepository = GetRepository(Context);
        DatabaseInfo? entity = await Context.DatabaseInfoes!.FirstOrDefaultAsync();
        //Act
        await databaseInfoRepository.DeleteAsync(entity!);
        Context.ChangeTracker.Clear();

        //Assert
        Context.DatabaseInfoes!.Any(item=>item.ID==entity!.ID).Should().BeFalse();

    }

    [Fact]
    public async Task SaveAsync_ShouldSaveOrModifyEntity()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture().DatabaseContext;
        DatabaseInfoRepository databaseInfoRepository = GetRepository(Context);
        DatabaseInfo databaseInfo = DatabaseInfoFaker.FakerRule().Generate();

        //Act (Adding then Updating)
        await databaseInfoRepository.SaveItemAsync(databaseInfo);
        databaseInfo.Name = new Faker().Random.Word();
        await databaseInfoRepository.SaveItemAsync(databaseInfo);
        Context.ChangeTracker.Clear();

        //Assert
        DatabaseInfo? savedEntity=await Context.DatabaseInfoes!.FindAsync(databaseInfo.ID);
        savedEntity.Should().NotBeNull();
        savedEntity.ID.Should().Be(databaseInfo.ID);
        savedEntity.Name.Should().Be(databaseInfo.Name);


    }


    [Fact]
    public async Task GetAllActiveDatabases_ShouldReturnOnlyActiveEntities()
    {
        //Arrange
        using AppDBContext Context = new AppDBContextFixture(seedDatabase: true).DatabaseContext;
        DatabaseInfoRepository databaseInfoRepository = GetRepository(Context);

        //Act
        List<DatabaseInfo> activeDatabases= databaseInfoRepository.GetAllDatabases();

        //Assert
        List<DatabaseInfo> retrievedActiveDatabases=await Context.DatabaseInfoes!.Where(item=>item.IsActive).ToListAsync();
        retrievedActiveDatabases.Count.Should().Be(activeDatabases.Count);
    }



}
