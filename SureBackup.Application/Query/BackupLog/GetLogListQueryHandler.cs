
using MediatR;
using SureBackup.Application.Repository;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupLog;

public class GetLogListQueryHandler(ILogRepository logRepository) : IRequestHandler<GetLogListQuery, List<Log>>
{
    public Task<List<Log>> Handle(GetLogListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(logRepository.GetLogs(request.PageSize, request.Page).ToList());
    }
}
