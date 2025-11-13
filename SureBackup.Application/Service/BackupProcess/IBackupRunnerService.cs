namespace SureBackup.Application.Service.BackupProcess;

public interface IBackupRunnerService
{
    Task RunBackupProcess(Action<string> progressLog, Action onBatchProcessFinished);
}
