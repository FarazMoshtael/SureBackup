
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Service.FTP;

public interface IFTPProcessService
{
    public void SetupFTPCredentials(BackupSetting setting);
    public Task<Result> CheckConnection();
    public Task<Result> ManageHostForRemainingStorage(string currentBackupFilePath);
    public Task<Result> UploadFile(string filePath, Action<double> onUploadProgressUpdated);

}
