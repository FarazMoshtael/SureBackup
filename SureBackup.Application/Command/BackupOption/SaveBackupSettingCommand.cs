

using MediatR;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.BackupOption;

public record SaveBackupSettingCommand(int IntervalMiliseconds,string BackupOperationPath,long? HostSizeBytes,bool FTPUpload,
    string? FTPUrl,string? FTPUsername,string? FTPPassword,string? BackupKey,bool EncryptionBackup):IRequest<Result>;