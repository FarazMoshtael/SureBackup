

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SureBackup.Application.Common;
using SureBackup.Application.Service.DBBackup;
using SureBackup.Application.Service.Wrapper;
using SureBackup.Domain.Common;
using SureBackup.Domain.Enums;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Command.DBBackup;

public class DBBackupCommandHandler([FromKeyedServices(Constants.Service.SQLServerBackupService)] IDBBackupService sqlServerbackupService,
    [FromKeyedServices(Constants.Service.MySQLBackupService)] IDBBackupService mysqlBackupService,ILogger<DBBackupCommandHandler> logger,IDirectoryWrapper directoryWrapper) : IRequestHandler<DBBackupCommand, Result<string>>
{
    public Task<Result<string>> Handle(DBBackupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.BackupSetting.BackupOperationPath) || !directoryWrapper.Exists(request.BackupSetting.BackupOperationPath))
                return Task.FromResult(Result<string>.Failure(ApplicationMessages.BackupSetting.UnknownOperationDirectoryPath));
         

            Result<string> backupProcessResult = request.DatabaseInfo.Database switch
            {
                Domain.Enums.Database.SQLServer => sqlServerbackupService.BackupDatabase(request.DatabaseInfo, request.BackupSetting.BackupOperationPath),
                Domain.Enums.Database.MySQL => mysqlBackupService.BackupDatabase(request.DatabaseInfo, request.BackupSetting.BackupOperationPath),
                _ => Result<string>.Failure("Unknown database type.")
            };
            return Task.FromResult(backupProcessResult);
        }
        catch (Exception ex)
        {
            logger.LogError($"DB backup command handler error: {ex.Message}");
            return Task.FromResult(Result<string>.Failure($"{DomainMessages.DBBackup.HandlerError} DB Name : {request.DatabaseInfo.Name}"));
        }
       

    }
}
