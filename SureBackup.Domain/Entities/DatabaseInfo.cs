

using SureBackup.Domain.Common;
using SureBackup.Domain.Enums;

namespace SureBackup.Domain.Entities;

public partial class DatabaseInfo : Entity
{
    private string _name = string.Empty;
    public  string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(DomainMessages.DatabaseInfo.EmptyNameArg);
            _name = value;
        }
    }
    public Database Database { get; set; }
    private string _encryptedConnectionString = string.Empty;
    public  string EncryptedConnectionString
    {
        get => _encryptedConnectionString;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(DomainMessages.DatabaseInfo.EmptyConnectionStringArg);
            _encryptedConnectionString = value;
        }
    }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Log> BackupLogs { get; set; } = [];
}
