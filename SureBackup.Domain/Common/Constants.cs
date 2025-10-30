

namespace SureBackup.Domain.Common;
/// <summary>
/// Application constant properties
/// </summary>
public static class Constants
{

    public const int MinuteMilisecond = 60000;
    public const int HourMilisecond = 3600000;
    public const int DayMilisecond = 43200000;
    public const int MaximumDaysInMonth = 31;
    public const string DefaultDateFormat = "yyyy-MM-dd HH:mm ZZ";
    public const string NotAvailable = "N/A";
    public const string Available = "Available";
    public const int KeySize = 16;


    public static class Service
    {
        public const string SQLServerBackupService = "SQLServerBackup";
        public const string MySQLBackupService = "MySQLBackup";
        public const string MainWindowTabNavigationService = "MainWindowTabNavigationService";

    }
    public static class Database
    {
        public const string SQLServerBackupFileExtension = "bak";
        public const string MySQLBackupFileExtension = "sql";
        public const string EncryptedBackupFileExtension = "sur";
    }
    /// <summary>
    /// Constant setting items
    /// </summary>
    public static class Setting
    {
        /// <summary>
        /// The default backup setting interval which would be 216,000 Miliseconds equals to 6 hours.
        /// </summary>
        public const int DefaultInterval = HourMilisecond;
       
    }

 

}
