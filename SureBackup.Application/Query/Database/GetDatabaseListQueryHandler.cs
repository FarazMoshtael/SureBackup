
using MediatR;
using SureBackup.Application.Repository;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.Database;

public class GetDatabaseListQueryHandler(IDatabaseInfoRepository databaseInfoRepository) : IRequestHandler<GetDatabaseListQuery, List<DatabaseInfo>>
{
    public Task<List<DatabaseInfo>> Handle(GetDatabaseListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(databaseInfoRepository.GetAllDatabases());
    }
}
