
using SureBackup.Domain.Common;
using SureBackup.Domain.ExtensionMethods;
using System.ComponentModel.DataAnnotations;

namespace SureBackup.Domain.Entities;

public partial class BackupSetting : Entity
{
    private int _intervalInMiliseconds=Constants.Setting.DefaultInterval;
    public int IntervalMiliseconds
    {
        get => _intervalInMiliseconds;
        set
        {
            if (value <= 0)
                throw new ArgumentException(DomainMessages.BackupSetting.InvalidInterval);
            _intervalInMiliseconds = value;
        }
    }
    private string _backupOperationPath = string.Empty;
    [RegularExpression("^(?![\\\\/]{1}$)(?:[a-zA-Z]:\\\\|/)?(?:[^<>:\"|?*\\n\\r/\\\\]+[\\\\/])*[^<>:\"|?*\\n\\r/\\\\]*$")]
    public string BackupOperationPath
    {
        get => _backupOperationPath; set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(DomainMessages.BackupSetting.InvalidBackupOperationPath);
            _backupOperationPath = value;

            this.ValidateRegex(nameof(BackupOperationPath), value);
        }
    }
    public bool FTPUpload { get; set; }
    public long? HostSizeInBytes { get; set; }
    public string? FTPEncryptedUrl { get; set; }
    public string? FTPEncryptedUsername { get; set; }
    public string? FTPEncryptedPassword { get; set; }
    public string? EncryptedBackupKey { get; set; }
    public bool EncryptionBackup { get; set; }

}
