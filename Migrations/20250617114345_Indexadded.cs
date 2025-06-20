using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VagueVault.Migrations
{
    /// <inheritdoc />
    public partial class Indexadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlist_UserId",
                table: "Wishlist");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_UserId_ProductId",
                table: "Wishlist",
                columns: new[] { "UserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlist_UserId_ProductId",
                table: "Wishlist");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_UserId",
                table: "Wishlist",
                column: "UserId");
        }
    }
}
