

using MediatR;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.FileCryption;

public record FileDecryptionCommand(string? SourcePath,string? DestinationPath,Domain.Enums.Database Database):IRequest<Result>;