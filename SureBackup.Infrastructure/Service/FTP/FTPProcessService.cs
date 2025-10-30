

using Microsoft.Extensions.Logging;
using SureBackup.Application.Service.FTP;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Exceptions;
using SureBackup.Domain.Pattern;

namespace SureBackup.Infrastructure.Service.FTP;

public class FTPProcessService(ILogger<FTPProcessService> logger, IFTPClient ftpClient) : IFTPProcessService
{
    private BackupSetting? _setting;

    public void SetupFTPCredentials(BackupSetting setting)
    {
        if (!setting.FTPCredentialsAvailable)
            throw new UnknownEntityException("FTP credentials");

        _setting = setting;
    }

    public async Task<Result> CheckConnection() => await ftpClient.ConnectAsync();



    public async Task<Result> ManageHostForRemainingStorage(string currentBackupFilePath)
    {
        try
        {

            var filesResult = await ftpClient.GetDirectoryFiles();
            if (!filesResult.Success)
                return Result.Failure(filesResult.Message!);
            var fileInfo = new FileInfo(currentBackupFilePath);

            var usedSpace = (filesResult.Data!.Any() ? filesResult.Data!.Sum(file => file.Size) : 0) + fileInfo.Length;
            if (usedSpace <= _setting!.HostSizeInBytes)
            {
                var oldestDateFiles = filesResult.Data!.GroupBy(file => file.Modified.Date).OrderBy(File => File.Key).FirstOrDefault();
                if (oldestDateFiles is not null)
                    foreach (var file in oldestDateFiles)
                    {
                        Result removingFileResult = await ftpClient.DeleteAsync(file.Name);
                        if(!removingFileResult.Success)
                            return removingFileResult;
                    }

            }
            return Result.Successful();
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP manage host storage error: {ex.Message}");
            return Result.Failure($"FTP host management error occured. {ex.Message}");
        }
    }



    public async Task<Result> UploadFile(string filePath, Action<double> onUploadProgressUpdated)
    {
        try
        {

            onUploadProgressUpdated?.Invoke(0);
            var fileInfo = new FileInfo(filePath);

            // define the progress tracking callback

            Progress<double> uploadProgress = new Progress<double>(percentage =>
            {
                onUploadProgressUpdated?.Invoke(percentage);

            });
            // upload a file with progress tracking
            Result uploadResult = await ftpClient.UploadFileAsync(filePath, fileInfo.Name, uploadProgress);
            return uploadResult;
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP upload error: {ex.Message}");
            return Result.Failure($"FTP error occured. {ex.Message}");
        }
    }





}
