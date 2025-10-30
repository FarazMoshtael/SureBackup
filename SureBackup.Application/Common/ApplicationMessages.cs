
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
    }
    public static class DatabaseInfo
    {
        public const string SaveDatabaseError = "An error occured while saving database info.";
        public const string UnknownDatabaseInfo = "The database info could not be found.";
        public const string EmptyConnectionString = "The connection string of database has not been set.";
    }
}
