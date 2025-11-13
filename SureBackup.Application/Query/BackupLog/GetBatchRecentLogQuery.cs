
using MediatR;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupLog;

public record GetBatchRecentLogQuery:IRequest<List<Log>>;
