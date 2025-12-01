using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureBackup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FTPLocalFileRemovalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FTPUploadLocalFileRemoval",
                table: "BackupSetting",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FTPUploadLocalFileRemoval",
                table: "BackupSetting");
        }
    }
}
