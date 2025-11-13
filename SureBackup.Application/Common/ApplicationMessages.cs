
namespace SureBackup.Application.Common;

public static class ApplicationMessages
{
    public static class FTPProcessService
    {
        public const string FTPUploadError = "An error occured while uploading file to FTP host.";
        public const string UnavailableFTPSetting = "The backup FTP options has not been set.";

    }
    public static class BackupSetting
    {
        public const string UnknownOperationDirectoryPath = "The backup directory path is not defined.";
        public const string SavingProcessError = "An error occured while saving backup setting.";
        public const string TooLateInterval = "The interval is too late.";
        public const string SuccessfulSavingBackupSetting = "The backup setting is saved successfully.";
        public const string InvalidBackupOperationPath = "The backup operation path is not valid.";
        public const string InvalidKeySize = "the backup encryption key size is not valid.";

    }
    public static class DatabaseInfo
    {
        public const string SaveDatabaseError = "An error occured while saving database info.";
        public const string UnknownDatabaseInfo = "The database info could not be found.";
        public const string EmptyConnectionString = "The connection string of database has not been set.";
        public const string SuccessfulSave = "The database info is saved successfully.";
    }
    public static class FileCryption
    {
        public const string EncryptionError = "An error occured while encrypting the file.";
        public const string UnavailableEncryptionSetting = "The backup encryption option has not been set.";
        public const string DecryptionError = "An error occured while decrypting the file.";
        public const string UnknownSourceFileError = "The source backup file is not valid.";
        public const string UnknownDestinationPathError = "The destination path is not valid.";
        public const string SuccessfulRestore = "The backup file restored successfully.";
    }
}
