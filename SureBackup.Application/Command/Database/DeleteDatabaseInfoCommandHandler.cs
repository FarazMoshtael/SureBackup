
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Repository;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.Database;

public class DeleteDatabaseInfoCommandHandler(IDatabaseInfoRepository databaseInfoRepository,ILogger<DeleteDatabaseInfoCommandHandler> logger) : IRequestHandler<DeleteDatabaseInfoCommand, Result>
{
    public async Task<Result> Handle(DeleteDatabaseInfoCommand request, CancellationToken cancellationToken)
    {
        DatabaseInfo? database = databaseInfoRepository.QueryableItems().SingleOrDefault(item=>item.ID==request.DatabaseInfoID);
        if (database is null)
            return Result.Failure("The database info could not be found to be removed.");

       await databaseInfoRepository.DeleteAsync(database);
        return Result.Successful($"Database info \"{database.Name}\" is removed successfully.");
    }
}
