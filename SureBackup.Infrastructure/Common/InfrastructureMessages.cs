

namespace SureBackup.Infrastructure.Common;

public static class InfrastructureMessages
{
    public static class FTPProcessService
    {
        public const string FTPUploadError = "FTP file upload is failed.";
    }

    public static class FTPClient
    {
        public const string ConnectionFailure = "The FTP client connection is failed.";
        public const string DisconnectionFailure = "The FTP client disconnection is failed.";
        public const string FileDeleteFailure = "The file could not be removed from FTP host.";
        public const string NoConnectionError = "The FTP client is not connected.";
        public const string ListingFilesFailure= "The FTP client could not list remote files.";
        public const string UnknownFTPCredentialsError = "The FTP client credentials is not defined.";
    }
}
