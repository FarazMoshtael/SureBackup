
using MediatR;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupLog;

public record GetRecentLogQuery:IRequest<Log?>;
