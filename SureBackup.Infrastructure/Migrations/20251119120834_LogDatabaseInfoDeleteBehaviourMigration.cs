using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SureBackup.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LogDatabaseInfoDeleteBehaviourMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Log_DatabaseInfo_DatabaseInfoID", table: "Log");
            migrationBuilder.AddForeignKey(
                name: "FK_Log_DatabaseInfo_DatabaseInfoID",
                table:"Log",
                column:"DatabaseInfoID",
                principalTable:"DatabaseInfo",
                principalColumn:"ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Log_DatabaseInfo_DatabaseInfoID", table: "Log");

        }
    }
}
