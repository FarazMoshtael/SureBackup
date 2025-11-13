

using Azure;
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
    public async Task<Result> NewErrorLogAsync(string message, DatabaseInfo databaseInfo, int batchNumber)
    {
        Log log = new()
        {
            Message = message,
            Type = AppLogType.Error,
            DatabaseInfoID = databaseInfo.ID,
            Date = DateTime.Now,
            BatchNumber = batchNumber
        };

        await AddAsync(log);
        return Result.Successful();
    }

    public async Task<Result> NewInformationLogAsync(string message, DatabaseInfo databaseInfo, int batchNumber)
    {
        Log log = new()
        {
            Message = message,
            Type = AppLogType.Information,
            DatabaseInfoID = databaseInfo.ID,
            Date = DateTime.Now,
            BatchNumber = batchNumber
        };
        await AddAsync(log);
        return Result.Successful();
    }

    public IQueryable<Log> GetLogs(int? PageSize = null, int? Page = null)
    {
        return QueryableItems(PageSize, Page,noTracking:true).Include(item => item.DatabaseInfo).OrderByDescending(item => item.Date);
    }

    public IList<Log> GetRecentLogs()
    {
        return QueryableItems(noTracking: true).Include(item => item.DatabaseInfo).GroupBy(item => item.BatchNumber).AsEnumerable().OrderByDescending(item => item.Key).FirstOrDefault()?.ToList()??[];
    }

    public int NewBatchNumber()
    {
        return (QueryableItems(noTracking:true).OrderByDescending(item => item.BatchNumber).FirstOrDefault()?.BatchNumber??0)+1;
    }
}
