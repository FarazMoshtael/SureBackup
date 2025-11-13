

using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Common;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.FileCryption;

public class FileDecryptionCommandHandler(IStreamCryptionService streamCryptionService, IFileWrapper fileWrapper,
    ILogger<FileDecryptionCommandHandler> logger, IDirectoryWrapper directoryWrapper, IBackupSettingRepository backupSettingRepository) : IRequestHandler<FileDecryptionCommand, Result>
{
    public Task<Result> Handle(FileDecryptionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.DestinationPath is null || !directoryWrapper.Exists(request.DestinationPath))
                return Task.FromResult(Result.Failure(ApplicationMessages.FileCryption.UnknownDestinationPathError));

            if (request.SourcePath is null || !fileWrapper.Exists(request.SourcePath) ||
                fileWrapper.GetFileNameExtension(request.SourcePath)!=$".{Constants.Database.EncryptedBackupFileExtension}")
                return Task.FromResult(Result.Failure(ApplicationMessages.FileCryption.UnknownSourceFileError));

            BackupSetting backupSetting = backupSettingRepository.GetAvailableBackupSetting();
            string backupExtension = request.Database switch
            {
                Domain.Enums.Database.SQLServer => Constants.Database.SQLServerBackupFileExtension,
                Domain.Enums.Database.MySQL => Constants.Database.MySQLBackupFileExtension,
                _ => string.Empty
            };

            using Stream sourceFileStream = fileWrapper.OpenReadFile(request.SourcePath);
            using Stream destinationFileStream = fileWrapper.CreateFile($"{request.DestinationPath}\\{fileWrapper.GetFileNameWithoutExtension(request.SourcePath)}.{backupExtension}");

            streamCryptionService.Decrypt(sourceFileStream, destinationFileStream, backupSetting.BackupKey??string.Empty);
            return Task.FromResult(Result.Successful(ApplicationMessages.FileCryption.SuccessfulRestore));

        }
        catch (Exception ex)
        {
            logger.LogError(message: $"File decryption command handler error: {ex.Message}");
            return Task.FromResult(Result.Failure($"{ApplicationMessages.FileCryption.DecryptionError} File path: {request.SourcePath}"));
        }
    }
}
