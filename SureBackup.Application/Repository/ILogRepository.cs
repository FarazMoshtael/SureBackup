
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Repository;

public interface ILogRepository:IBaseRepository<Log>
{
    public Task<Result> NewInformationLogAsync(string message, DatabaseInfo databaseInfo,int batchNumber);
    public Task<Result> NewErrorLogAsync(string message, DatabaseInfo databaseInfo, int batchNumber);
    public IQueryable<Log> GetLogs(int? PageSize = null, int? Page = null);
    public IList<Log> GetRecentLogs();

    public int NewBatchNumber();
}
