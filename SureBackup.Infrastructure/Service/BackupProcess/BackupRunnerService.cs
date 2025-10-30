
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Command.DBBackup;
using SureBackup.Application.Command.FileCryption;
using SureBackup.Application.Command.FTP;
using SureBackup.Application.Repository;
using SureBackup.Application.Service.BackupProcess;
using SureBackup.Domain.Entities;

namespace SureBackup.Infrastructure.Service.BackupProcess;

public class BackupRunnerService(IDatabaseInfoRepository databaseInfoRepository, IBackupSettingRepository backupSettingRepository,
    ILogger<BackupRunnerService> logger, IMediator mediator, ILogRepository logRepository) : IBackupRunnerService
{
    public async Task RunBackupProcess(Action<double> onUploadProgressUpdated, Action<string> updateStatus)
    {
        try
        {
            BackupSetting setting = backupSettingRepository.GetAvailableBackupSetting();
            List<DatabaseInfo> availableDatabases = databaseInfoRepository.GetAllDatabases();
            updateStatus?.Invoke("Backup process is started ...");
            foreach (DatabaseInfo databaseInfo in availableDatabases)
            {
                updateStatus?.Invoke($"The Database named '{databaseInfo}' is entered for backup process ...");

                //Step 1: Backing up database
                var DBBackupProcessResult = await mediator.Send(new DBBackupCommand(databaseInfo, setting));
                if (!DBBackupProcessResult.Success)
                {
                    await logRepository.NewErrorLogAsync(DBBackupProcessResult.Message!, databaseInfo);
                    continue;
                }

                //Step 2: Going for Encryption if allowed
                var backupEncryptionResult = await mediator.Send(new FileEnryptionCommand(DBBackupProcessResult.Data!, setting));
                if (!backupEncryptionResult.Success)
                {
                    await logRepository.NewErrorLogAsync(backupEncryptionResult.Message!, databaseInfo);
                    continue;
                }
                //Step 3: Going for upload to FTP Host
                var FTPUploadResult = await mediator.Send(new FTPUploadCommand(backupEncryptionResult.Data!, setting, onUploadProgressUpdated));
                if (!backupEncryptionResult.Success)
                {
                    await logRepository.NewErrorLogAsync(backupEncryptionResult.Message!, databaseInfo);
                    continue;
                }
                await logRepository.NewInformationLogAsync($"Successful database backup: {databaseInfo.Name}", databaseInfo);

            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Run backup background service handler error: {ex.Message}");
        }
    }
}
