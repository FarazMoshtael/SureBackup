
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Common;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.Cryption;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.Database;

public class SaveDatabaseInfoCommandHandler(IDatabaseInfoRepository databaseInfoRepository,ILogger<SaveDatabaseInfoCommandHandler> logger,
    ITextCryptionService textCryptionService) : IRequestHandler<SaveDatabaseInfoCommand, Result>
{
    public async Task<Result> Handle(SaveDatabaseInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            DatabaseInfo? database=new();
            if (request.ID != default)
                database = databaseInfoRepository.QueryableItems().SingleOrDefault(item => item.ID == request.ID);
            if(database is null)
                return Result.Failure(ApplicationMessages.DatabaseInfo.UnknownDatabaseInfo);
            if (string.IsNullOrWhiteSpace(request.ConnectionString))
                return Result.Failure(ApplicationMessages.DatabaseInfo.EmptyConnectionString);

            database.EncryptedConnectionString=textCryptionService.Encrypt(request.ConnectionString);
            database.Name = request.Name;
            database.Database=request.DatabaseType;
            database.IsActive=request.IsActive;
            await databaseInfoRepository.SaveItemAsync(database);
            return Result.Successful();


        }
        catch (Exception ex)
        {
            logger.LogError($"Save database info error: {ex.Message}");
            return Result.Failure(ApplicationMessages.DatabaseInfo.SaveDatabaseError);
        }
    }
}
