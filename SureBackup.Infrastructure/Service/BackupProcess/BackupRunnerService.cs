
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Command.DBBackup;
using SureBackup.Application.Command.FileCryption;
using SureBackup.Application.Command.FTP;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.BackupProcess;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Entities;

namespace SureBackup.Infrastructure.Service.BackupProcess;

public class BackupRunnerService(IDatabaseInfoRepository databaseInfoRepository, IBackupSettingRepository backupSettingRepository,
    ILogger<BackupRunnerService> logger, IMediator mediator, ILogRepository logRepository, IFileWrapper fileWrapper) : IBackupRunnerService
{
    public async Task RunBackupProcess(Action<string> updateStatus, Action onBatchProcessFinished)
    {
        try
        {
            BackupSetting setting = backupSettingRepository.GetAvailableBackupSetting();
            List<DatabaseInfo> availableDatabases = databaseInfoRepository.GetAllDatabases(isActive:true);

            int batchNumber = logRepository.NewBatchNumber();

            foreach (DatabaseInfo databaseInfo in availableDatabases)
            {
                updateStatus?.Invoke($"The database named '{databaseInfo.Name}' is entered for backup process ...");

                //Step 1: Backing up database
                var DBBackupProcessResult = await mediator.Send(new DBBackupCommand(databaseInfo, setting));
                if (!DBBackupProcessResult.Success)
                {
                    await logRepository.NewErrorLogAsync(DBBackupProcessResult.Message!, databaseInfo, batchNumber);
                    updateStatus?.Invoke(DBBackupProcessResult.Message!);
                    continue;
                }

                //Step 2: Going for Encryption if allowed
                var backupEncryptionResult = await mediator.Send(new FileEnryptionCommand(DBBackupProcessResult.Data!, setting));
                if (!backupEncryptionResult.Success)
                {

                    await logRepository.NewErrorLogAsync(backupEncryptionResult.Message!, databaseInfo, batchNumber);
                    updateStatus?.Invoke(backupEncryptionResult.Message!);
                    continue;
                }
                var fileSize = fileWrapper.GetFileSize(backupEncryptionResult.Data!);
                double lastProgressPercent = 0;
                //Step 3: Going for upload to FTP Host
                var FTPUploadResult = await mediator.Send(new FTPUploadCommand(backupEncryptionResult.Data!, setting, (progress) =>
                {
                    double percent = 0;
                    if (fileSize > 0)
                        percent = (progress * 100.0) / fileSize;
                    if (percent - lastProgressPercent > 5 || percent>=100)
                    {
                        updateStatus?.Invoke($"Uploading {databaseInfo.Name} database {percent.ToString("###.00")}% ...");

                        lastProgressPercent = percent;

                    }
                }));

                if (!FTPUploadResult.Success)
                {
                    await logRepository.NewErrorLogAsync(FTPUploadResult.Message!, databaseInfo, batchNumber);
                    updateStatus?.Invoke(FTPUploadResult.Message!);
                    continue;
                }
                await logRepository.NewInformationLogAsync($"Successful database backup: {databaseInfo.Name}", databaseInfo, batchNumber);
                updateStatus?.Invoke($"The database named '{databaseInfo.Name}' is successfully processed for backup.");

            }
            onBatchProcessFinished?.Invoke();
        }
        catch (Exception ex)
        {
            logger.LogError($"Run backup background service handler error: {ex.Message}");
        }
    }
}
