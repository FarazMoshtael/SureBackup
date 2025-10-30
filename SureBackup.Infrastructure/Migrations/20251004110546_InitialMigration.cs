using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureBackup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackupSetting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IntervalInMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    HostSizeInBytes = table.Column<long>(type: "INTEGER", nullable: true),
                    FTPEncryptedUrl = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    FTPEncryptedUsername = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    FTPEncryptedPassword = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackupSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Database = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectionString = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DatabaseInfoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Log_DatabaseInfo_DatabaseInfoID",
                        column: x => x.DatabaseInfoID,
                        principalTable: "DatabaseInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_DatabaseInfoID",
                table: "Log",
                column: "DatabaseInfoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackupSetting");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "DatabaseInfo");
        }
    }
}
