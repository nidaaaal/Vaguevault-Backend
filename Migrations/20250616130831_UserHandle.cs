using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VagueVault.Migrations
{
    public partial class UserHandle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ First: Add the StatusId column
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            // ✅ Then: Add the foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Status_StatusId",
                table: "Users",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key first
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Status_StatusId",
                table: "Users");

            // Then drop column
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Users");
        }
    }
}
