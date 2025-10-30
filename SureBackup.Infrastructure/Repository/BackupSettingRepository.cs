

using SureBackup.Application.Repository;
using SureBackup.Application.Service.Cryption;
using SureBackup.Domain.Entities;
using SureBackup.Domain.Exceptions;
using SureBackup.Infrastructure.Database;
using System.Linq.Expressions;

namespace SureBackup.Infrastructure.Repository;

public class BackupSettingRepository(ITextCryptionService textCryptionService, AppDBContext context) : BaseRepository<BackupSetting,int>(context), IBackupSettingRepository
{
  
    public override Expression<Func<BackupSetting, int>> GetKey()
    {
        return item => item.ID;
    }
    public BackupSetting GetAvailableBackupSetting()
    {
        BackupSetting? setting=EntityDBSet.FirstOrDefault();
        if(setting is null)
            return new BackupSetting();
        setting.FTPUrl = !string.IsNullOrEmpty(setting.FTPEncryptedUrl)? textCryptionService.Decrypt(setting.FTPEncryptedUrl!): string.Empty;
        setting.FTPUsername = !string.IsNullOrEmpty(setting.FTPEncryptedUsername) ? textCryptionService.Decrypt(setting.FTPEncryptedUsername!) : string.Empty;
        setting.FTPPassword = !string.IsNullOrEmpty(setting.FTPEncryptedPassword) ? textCryptionService.Decrypt(setting.FTPEncryptedPassword!) : string.Empty;
        setting.BackupKey = !string.IsNullOrEmpty(setting.EncryptedBackupKey) ? textCryptionService.Decrypt(setting.EncryptedBackupKey!) : string.Empty;

        return setting;

    }


}
