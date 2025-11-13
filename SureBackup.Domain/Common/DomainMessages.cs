
namespace SureBackup.Domain.Common;

public static class DomainMessages
{
    public static class BackupSetting
    {
        public const string InvalidInterval = "The interval value is invalid.";
        public const string EmptyBackupOperationPath = "The backup operation path is empty.";
    }
    public static class DatabaseInfo
    {
        public const string EmptyNameArg = "The database name is empty.";
        public const string EmptyConnectionStringArg = "The database connection string is empty.";

    }
    public static class Log
    {
        public const string EmptyMessageArg = "The log message is empty.";
    }


    public static class DBBackup
    {
        public const string HandlerError = "An error occured while backing up database.";
    }
 
}
