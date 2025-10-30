

using Microsoft.EntityFrameworkCore;
using SureBackup.Application.Repository;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Enums;
using SureBackup.Domain.Pattern;
using SureBackup.Infrastructure.Database;
using System.Linq.Expressions;

namespace SureBackup.Infrastructure.Repository;

public class LogRepository : BaseRepository<Log, int>, ILogRepository
{
    public LogRepository(AppDBContext context) : base(context)
    {
    }

    public override Expression<Func<Log, int>> GetKey()
    {
        return item => item.ID;
    }
    public async Task<Result> NewErrorLogAsync(string message, DatabaseInfo databaseInfo)
    {
        Log log = new()
        {
            Message = message,
            Type = AppLogType.Error,
            DatabaseInfoID = databaseInfo.ID,
            Date = DateTime.Now,
        };

        await AddAsync(log);
        return Result.Successful();
    }

    public async Task<Result> NewInformationLogAsync(string message, DatabaseInfo databaseInfo)
    {
        Log log = new()
        {
            Message = message,
            Type = AppLogType.Information,
            DatabaseInfoID = databaseInfo.ID,
            Date = DateTime.Now,
        };
        await AddAsync(log);
        return Result.Successful();
    }

    public IQueryable<Log> GetLogs(int? PageSize = null, int? Page = null)
    {
        return QueryableItems(PageSize, Page).OrderByDescending(item => item.Date).Include(item => item.DatabaseInfo);
    }
}
