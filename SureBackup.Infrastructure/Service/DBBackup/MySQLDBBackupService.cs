

using MySql.Data.MySqlClient;
using SureBackup.Application.Service.Cryption;
using SureBackup.Application.Service.DBBackup;
using SureBackup.Domain.Common;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;

namespace SureBackup.Infrastructure.Service.DBBackup;

public class MySQLDBBackupService(ITextCryptionService textCryptionService) : IDBBackupService
{
    public Result<string> BackupDatabase(DatabaseInfo databaseInfo, string destinationPath)
    {

        databaseInfo.ConnectionString = textCryptionService.Decrypt(databaseInfo.EncryptedConnectionString);
        var backupFilePath = $"{destinationPath}\\{databaseInfo.Name}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.{Constants.Database.MySQLBackupFileExtension}";
        using (MySqlConnection conn = new MySqlConnection(databaseInfo.ConnectionString))
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    mb.ExportToFile(backupFilePath);
                    conn.Close();
                }
            }
        }
        return Result<string>.Successful(backupFilePath);


    }
}
