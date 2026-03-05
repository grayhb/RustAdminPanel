using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustAdminPanel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLastServerConnectionAtInPlayerProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastServerConnectionAt",
                table: "PlayerProfiles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastServerConnectionAt",
                table: "PlayerProfiles");
        }
    }
}
