

using MediatR;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.Database;

public record SaveDatabaseInfoCommand(int ID,string Name, Domain.Enums.Database DatabaseType,string ConnectionString,bool IsActive):IRequest<Result>;