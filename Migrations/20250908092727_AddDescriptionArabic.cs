using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BethanysPieShopAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionArabic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionArabic",
                table: "Categories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionArabic",
                table: "Categories");
        }
    }
}
