

using MediatR;
using SureBackup.Application.Repository;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupLog;

public class GetBatchRecentLogQueryHandler(ILogRepository logRepository) : IRequestHandler<GetBatchRecentLogQuery, List<Log>>
{
    public Task<List<Log>> Handle(GetBatchRecentLogQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(logRepository.GetRecentLogs().ToList());
    }
}
