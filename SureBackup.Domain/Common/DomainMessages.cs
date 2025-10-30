
namespace SureBackup.Domain.Common;

public static class DomainMessages
{
    public static class BackupSetting
    {
        public const string InvalidInterval = "The interval value is invalid.";
        public const string SavingProcessError = "An error occured while saving backup setting.";
        public const string InvalidBackupOperationPath = "The backup operation path is not valid";
        public const string TooLateInterval = "The interval is too late.";
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
    public static class FileCryption
    {
        public const string EncryptionError = "An error occured while encrypting the file.";
        public const string UnavailableEncryptionSetting = "The backup encryption option has not been set.";

    }
}
