using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class RemoveAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CreatedAd",
                table: "Messages",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAd",
                table: "Logs",
                newName: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Messages",
                newName: "CreatedAd");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Logs",
                newName: "CreatedAd");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
