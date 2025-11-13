

using SureBackup.Domain.Common;
using SureBackup.Domain.Enums;

namespace SureBackup.Domain.Entities;

public class Log : Entity
{
    public DateTime Date { get; set; }
    private string _message = string.Empty;
    public required string Message
    {
        get => _message; set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(DomainMessages.Log.EmptyMessageArg);
            _message = value;
        }
    }
    public int DatabaseInfoID { get; set; }
    public AppLogType Type { get; set; }
    public int BatchNumber { get; set; } = 1; 
    public virtual DatabaseInfo? DatabaseInfo { get; set; }

}
