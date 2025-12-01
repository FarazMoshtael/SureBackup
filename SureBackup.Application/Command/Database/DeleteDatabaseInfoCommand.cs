

using MediatR;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.Database;

public record DeleteDatabaseInfoCommand(int DatabaseInfoID):IRequest<Result>;