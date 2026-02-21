using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustAdminPanel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConnectionDateTime",
                table: "PlayerConnectionLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Channel = table.Column<long>(type: "INTEGER", nullable: false),
                    SteamName = table.Column<string>(type: "TEXT", nullable: false),
                    SteamId = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<long>(type: "INTEGER", nullable: false),
                    MessageDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "ConnectionDateTime",
                table: "PlayerConnectionLogs");
        }
    }
}
