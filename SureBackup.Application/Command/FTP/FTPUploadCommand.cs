

using MediatR;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.FTP;

public record FTPUploadCommand(string FilePath,BackupSetting BackupSetting,Action<double> OnUploadProgressUpdated):IRequest<Result>;