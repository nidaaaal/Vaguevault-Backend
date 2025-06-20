using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VagueVault.Migrations
{
    /// <inheritdoc />
    public partial class AddressModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Address",
                newName: "Street");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Address",
                newName: "State");
        }
    }
}
