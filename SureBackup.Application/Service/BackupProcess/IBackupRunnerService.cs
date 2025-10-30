namespace SureBackup.Application.Service.BackupProcess;

public interface IBackupRunnerService
{
    Task RunBackupProcess(Action<double> onUploadProgressUpdated,Action<string> updateStatus);
}
