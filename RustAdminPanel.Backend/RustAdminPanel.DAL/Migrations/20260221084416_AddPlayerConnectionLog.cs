using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustAdminPanel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerConnectionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerConnectionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SteamName = table.Column<string>(type: "TEXT", nullable: false),
                    SteamId = table.Column<string>(type: "TEXT", nullable: false),
                    ConnectionIp = table.Column<string>(type: "TEXT", nullable: false),
                    ConnectionTimestamp = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerConnectionLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerConnectionLogs");
        }
    }
}
