

using SureBackup.Domain.Entities;

namespace SureBackup.Application.Repository;

public interface IBackupSettingRepository:IBaseRepository<BackupSetting>
{
    public BackupSetting GetAvailableBackupSetting();
}
