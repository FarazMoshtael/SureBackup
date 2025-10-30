

namespace SureBackup.Domain.Entities;

public partial class BackupSetting
{
    public string? FTPUrl { get; set; }
    public string? FTPUsername { get; set; }
    public string? FTPPassword { get; set; }
    public bool FTPCredentialsAvailable=>!string.IsNullOrEmpty(FTPUrl)&&
        !string.IsNullOrEmpty(FTPUsername) && !string.IsNullOrEmpty(FTPPassword);
    public string? BackupKey {  get; set; }
    public bool EncryptionAvailable=>EncryptionBackup && !string.IsNullOrEmpty(BackupKey);
}
