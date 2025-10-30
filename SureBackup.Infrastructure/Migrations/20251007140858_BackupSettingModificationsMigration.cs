using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureBackup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BackupSettingModificationsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IntervalInMinutes",
                table: "BackupSetting",
                newName: "IntervalMiliseconds");

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionString",
                table: "DatabaseInfo",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedConnectionString",
                table: "DatabaseInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BackupKey",
                table: "BackupSetting",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackupOperationPath",
                table: "BackupSetting",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EncryptedBackupKey",
                table: "BackupSetting",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EncryptionBackup",
                table: "BackupSetting",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FTPPassword",
                table: "BackupSetting",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FTPUpload",
                table: "BackupSetting",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FTPUrl",
                table: "BackupSetting",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FTPUsername",
                table: "BackupSetting",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedConnectionString",
                table: "DatabaseInfo");

            migrationBuilder.DropColumn(
                name: "BackupKey",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "BackupOperationPath",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "EncryptedBackupKey",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "EncryptionBackup",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "FTPPassword",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "FTPUpload",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "FTPUrl",
                table: "BackupSetting");

            migrationBuilder.DropColumn(
                name: "FTPUsername",
                table: "BackupSetting");

            migrationBuilder.RenameColumn(
                name: "IntervalMiliseconds",
                table: "BackupSetting",
                newName: "IntervalInMinutes");

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionString",
                table: "DatabaseInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
