
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Repository;

public interface ILogRepository:IBaseRepository<Log>
{
    public Task<Result> NewInformationLogAsync(string message, DatabaseInfo databaseInfo);
    public Task<Result> NewErrorLogAsync(string message, DatabaseInfo databaseInfo);
    public IQueryable<Log> GetLogs(int? PageSize = null, int? Page = null);
}
