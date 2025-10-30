using SureBackup.Domain.Entities;
using SureBackup.Domain.Pattern;
using System.Data;
using Microsoft.Data.SqlClient;
using SureBackup.Application.Service.DBBackup;
using SureBackup.Application.Service.Cryption;
using SureBackup.Domain.Common;

namespace SureBackup.Infrastructure.Service.DBBackup;

public class SQLServerDBBackupService(ITextCryptionService textCryptionService) : IDBBackupService
{
    public Result<string> BackupDatabase(DatabaseInfo databaseInfo, string destinationPath)
    {

            databaseInfo.ConnectionString = textCryptionService.Decrypt(databaseInfo.EncryptedConnectionString);
            var backupFilePath = $"{destinationPath}\\{databaseInfo.Name}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}.{Constants.Database.SQLServerBackupFileExtension}";
            using (SqlConnection con = new SqlConnection(databaseInfo.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = $"BACKUP DATABASE {databaseInfo.Name} TO DISK ='{backupFilePath}'";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            return Result<string>.Successful(backupFilePath);

     
    }
}
