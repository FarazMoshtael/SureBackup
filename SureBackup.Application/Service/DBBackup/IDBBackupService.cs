using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Application.Service.DBBackup;

public interface IDBBackupService
{
    public Result<string> BackupDatabase(DatabaseInfo databaseInfo, string destinationPath);
}
