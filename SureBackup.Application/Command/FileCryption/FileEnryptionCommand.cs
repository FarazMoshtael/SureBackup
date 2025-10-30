

using MediatR;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.FileCryption;

public record FileEnryptionCommand(string FilePath,BackupSetting BackupSetting):IRequest<Result<string>>;
