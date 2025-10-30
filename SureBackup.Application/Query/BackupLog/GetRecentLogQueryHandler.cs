

using MediatR;
using SureBackup.Application.Repository;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupLog;

public class GetRecentLogQueryHandler(ILogRepository logRepository) : IRequestHandler<GetRecentLogQuery, Log?>
{
    public Task<Log?> Handle(GetRecentLogQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(logRepository.GetLogs().FirstOrDefault());
    }
}
