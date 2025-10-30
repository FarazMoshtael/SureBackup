

using MediatR;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;
namespace SureBackup.Application.Command.DBBackup;

public record DBBackupCommand(DatabaseInfo DatabaseInfo,BackupSetting BackupSetting):IRequest<Result<string>>;
