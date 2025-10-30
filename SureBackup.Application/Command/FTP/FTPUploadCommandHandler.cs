
using MediatR;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Common;
using SureBackup.Application.Service.FTP;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.FTP;

public class FTPUploadCommandHandler(IFTPProcessService FTPService, ILogger<FTPUploadCommandHandler> logger) : IRequestHandler<FTPUploadCommand, Result>
{
    public async Task<Result> Handle(FTPUploadCommand request, CancellationToken cancellationToken)
    {
        try
        {

            if (!request.BackupSetting.FTPCredentialsAvailable)
            {
                //Step 1: FTP credentials setup 
                FTPService.SetupFTPCredentials(request.BackupSetting);

                //Step 2: Check if FTP can connect by credentials
                var FTPConnectionResult = await FTPService.CheckConnection();
                if (!FTPConnectionResult.Success)
                    return await Task.FromResult(FTPConnectionResult);

                //Step 3: Manage host files and remove old backup files if needed
                var FTPHostStorageManagementResult = await FTPService.ManageHostForRemainingStorage(request.FilePath);
                if (!FTPConnectionResult.Success)
                    return await Task.FromResult(FTPHostStorageManagementResult);

                //Step 4: Uploading the backup file to FTP host
                var FTPUploadResult = await FTPService.UploadFile(request.FilePath, request.OnUploadProgressUpdated);
                return await Task.FromResult(FTPUploadResult);
            }

            return await Task.FromResult(Result.Successful());
        }
        catch (Exception ex)
        {
            logger.LogError($"FTP upload handler error: {ex.Message}");
            return await Task.FromResult(Result.Failure($"{ApplicationMessages.FTPProcessService.FTPUploadError} File path: {request.FilePath}"));
        }
    }
}
