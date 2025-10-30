
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Common;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.FileCryption;

public class FileEncryptionCommandHandler(IStreamCryptionService fileCryptionService, ILogger<FileEncryptionCommandHandler> logger,
    IFileWrapper fileWrapper) : IRequestHandler<FileEnryptionCommand, Result<string>>
{
    public Task<Result<string>> Handle(FileEnryptionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.BackupSetting.EncryptionAvailable)
            {
                var encryptedFilePath = $"{request.BackupSetting.BackupOperationPath}\\{fileWrapper.GetFileNameWithoutExtension(request.FilePath)}.{Constants.Database.EncryptedBackupFileExtension}";
                using Stream sourceFile = fileWrapper.OpenReadFile(request.FilePath);
                using Stream destinationFile = fileWrapper.CreateFile(encryptedFilePath);
                fileCryptionService.Encrypt(sourceFile, destinationFile, request.BackupSetting.BackupKey!);
                sourceFile.Flush();
                sourceFile.Dispose();
                fileWrapper.DeleteFile(request.FilePath);
                return Task.FromResult(Result<string>.Successful(encryptedFilePath));
            }

            return Task.FromResult(Result<string>.Successful(request.FilePath));


        }
        catch (Exception ex)
        {
            logger.LogError($"File encryption handler error: {ex.Message}");
            return Task.FromResult(Result<string>.Failure($"{DomainMessages.FileCryption.EncryptionError} File path: {request.FilePath}"));
        }

    }
}
