

using FluentFTP;
using FluentFTP.Helpers;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Service.FTP;
using SureBackup.Domain.HelperModels;
using SureBackup.Domain.Pattern;
using SureBackup.Infrastructure.Common;

namespace SureBackup.Infrastructure.Service.FTP;

public class FTPClient(ILogger<FTPClient> logger) : IFTPClient
{
    private AsyncFtpClient? _ftpClient;
    public Result SetupFTPConnection(string ftpUrl, string userName, string password, CancellationToken token = default)
    {
        try
        {
            _ftpClient = new AsyncFtpClient(ftpUrl, userName, password);
            return Result.Successful();
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP client connection error: {ex.Message}");
            return Result.Failure(InfrastructureMessages.FTPClient.ConnectionFailure);
        }

    }

    public async Task<Result> ConnectAsync(CancellationToken token = default)
    {
        try
        {
            if (_ftpClient is null)
                return Result.Failure(InfrastructureMessages.FTPClient.UnknownFTPCredentialsError);
            if (!_ftpClient.IsConnected)
                await _ftpClient!.AutoConnect(token);
            return Result.Successful();
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP client connection error: {ex.Message}");
            return Result.Failure(InfrastructureMessages.FTPClient.ConnectionFailure);
        }

    }

    public async Task<Result> DeleteAsync(string remoteFilePath, CancellationToken token = default)
    {
        try
        {
            Result connectionResult = await ConnectAsync(token);
            if (connectionResult.Success)
                return connectionResult;

            await _ftpClient!.DeleteFile(remoteFilePath, token);
            await DisconnectAsync();
            return Result.Successful();
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP client file deleting error: {ex.Message}");
            return Result.Failure(InfrastructureMessages.FTPClient.FileDeleteFailure);
        }
    }

    public async Task<Result> DisconnectAsync(CancellationToken token = default)
    {
        try
        {
            if (_ftpClient is not null && _ftpClient.IsConnected)

                await _ftpClient!.Disconnect();
            return Result.Successful();


        }
        catch (Exception ex)
        {
            logger.LogError($"FTP client disconnection error: {ex.Message}");
            return Result.Failure(InfrastructureMessages.FTPClient.DisconnectionFailure);
        }
    }

    public async Task<Result<List<FTPFileInfo>>> GetDirectoryFiles(CancellationToken token = default)
    {
        try
        {
            Result connectionResult = await ConnectAsync(token);
            if (!connectionResult.Success)
                return Result<List<FTPFileInfo>>.Failure(connectionResult.Message!);

            List<FTPFileInfo> files = (await _ftpClient!.GetListing())
                          .Where(f => f.Type == FtpObjectType.File)
                          .OrderBy(f => f.Modified)
                          .Select(file => new FTPFileInfo
                          {
                              Name = file.Name,
                              Modified = file.Modified,
                              Size = file.Size
                          }).ToList();
            await DisconnectAsync();

            return Result<List<FTPFileInfo>>.Successful(files);


        }
        catch (Exception ex)
        {
            logger.LogError($"FTP client listing files error: {ex.Message}");
            return Result<List<FTPFileInfo>>.Failure(InfrastructureMessages.FTPClient.ListingFilesFailure);
        }
    }

    public async Task<Result> UploadFileAsync(string localFilePath, string remoteFilePath, IProgress<double> onUploadProgressUpdated, CancellationToken token = default)
    {
        try
        {
            Result connectionResult = await ConnectAsync(token);
            if (!connectionResult.Success)
                return Result.Failure(connectionResult.Message!);

            var ftpProgress = new Progress<FtpProgress>(p => onUploadProgressUpdated?.Report(p.TransferredBytes));
            FtpStatus uploadStatus = await _ftpClient!.UploadFile(localFilePath, remoteFilePath, FtpRemoteExists.Overwrite, false, FtpVerify.None, ftpProgress, token);
            return !uploadStatus.IsSuccess() ? Result.Failure(InfrastructureMessages.FTPProcessService.FTPUploadError) : Result.Successful();
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP client upload file error: {ex.Message}");
            return Result.Failure(InfrastructureMessages.FTPProcessService.FTPUploadError);
        }

    }

}
