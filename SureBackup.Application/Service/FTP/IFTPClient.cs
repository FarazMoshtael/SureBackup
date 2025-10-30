

using SureBackup.Domain.HelperModels;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Service.FTP;

public interface IFTPClient
{
    
    Task<Result> SetupFTPConnection(string ftpUrl,string userName,string password, CancellationToken token=default);
    Task<Result> ConnectAsync(CancellationToken token = default);
    Task<Result> DisconnectAsync(CancellationToken token = default);
    Task<Result> UploadFileAsync(string localFilePath,string remoteFilePath,IProgress<double> onUploadProgressUpdated, CancellationToken token = default);
    Task<Result<List<FTPFileInfo>>> GetDirectoryFiles(CancellationToken token = default);
    Task<Result> DeleteAsync(string remoteFilePath, CancellationToken token = default);
}
