

using MediatR;
using SureBackup.Domain.Entities;

namespace SureBackup.Application.Query.Database;

public record GetDatabaseListQuery(int? PageSize=null,int? Page=null):IRequest<List<DatabaseInfo>>;