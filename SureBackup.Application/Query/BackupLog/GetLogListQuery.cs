
using MediatR;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.BackupLog;

public record GetLogListQuery(int? PageSize=null,int? Page=null):IRequest<List<Log>>;
