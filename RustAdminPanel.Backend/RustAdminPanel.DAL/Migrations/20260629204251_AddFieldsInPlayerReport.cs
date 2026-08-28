using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RustAdminPanel.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInPlayerReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetId",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetName",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PlayerReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "PlayerReports");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerReports");

            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "PlayerReports");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "PlayerReports");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "PlayerReports");

            migrationBuilder.DropColumn(
                name: "TargetName",
                table: "PlayerReports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PlayerReports");
        }
    }
}
