
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Common;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.BackupOption;

public class SaveBackupSettingCommandHandler(IBackupSettingRepository backupSettingRepository, ITextCryptionService textCryptionService
    , ILogger<SaveBackupSettingCommandHandler> logger,IDirectoryWrapper directoryWrapper) : IRequestHandler<SaveBackupSettingCommand, Result>
{
    public async Task<Result> Handle(SaveBackupSettingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            BackupSetting? setting = backupSettingRepository.QueryableItems().FirstOrDefault();
            if (setting is null)
                setting = new();
            if (request.IntervalMiliseconds > Constants.DayMilisecond * Constants.MaximumDaysInMonth)
                return await Task.FromResult(Result.Failure(ApplicationMessages.BackupSetting.TooLateInterval));
            if (!directoryWrapper.Exists(request.BackupOperationPath))
                return await Task.FromResult(Result.Failure(ApplicationMessages.BackupSetting.InvalidBackupOperationPath));
            if(request.EncryptionBackup && request.BackupKey?.Length!=Constants.KeySize)
                return await Task.FromResult(Result.Failure(ApplicationMessages.BackupSetting.InvalidKeySize));


            setting.FTPUpload = request.FTPUpload;
            setting.EncryptionBackup = request.EncryptionBackup;

            if (!string.IsNullOrEmpty(request.BackupKey))
                setting.EncryptedBackupKey = textCryptionService.Encrypt(request.BackupKey);
            if (!string.IsNullOrEmpty(request.FTPUrl))
                setting.FTPEncryptedUrl = textCryptionService.Encrypt(request.FTPUrl);
            if (!string.IsNullOrEmpty(request.FTPUsername))
                setting.FTPEncryptedUsername = textCryptionService.Encrypt(request.FTPUsername);
            if (!string.IsNullOrEmpty(request.FTPPassword))
                setting.FTPEncryptedPassword = textCryptionService.Encrypt(request.FTPPassword);
      

            setting.HostSizeInBytes = request.HostSizeBytes;
            setting.IntervalMiliseconds = request.IntervalMiliseconds;

       
            setting.BackupOperationPath = request.BackupOperationPath;
            await backupSettingRepository.SaveItemAsync(setting);
            return await Task.FromResult(Result.Successful(ApplicationMessages.BackupSetting.SuccessfulSavingBackupSetting));
        }
        catch (Exception ex)
        {
            logger.LogError($"Save backup setting error: {ex.Message}");
            return await Task.FromResult(Result.Failure(ApplicationMessages.BackupSetting.SavingProcessError));
        }
    }
}
