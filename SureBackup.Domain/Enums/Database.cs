

using System.ComponentModel;

namespace SureBackup.Domain.Enums;

public enum Database:byte
{
    [Description("SQL Server")]
    SQLServer=0b0000,
    [Description("MySQL")]
    MySQL=0b0001,

}