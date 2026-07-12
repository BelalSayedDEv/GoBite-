using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoBite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToProductAddons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxQuantity",
                table: "ProductAddons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ProductAddons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxQuantity",
                table: "ProductAddons");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductAddons");
        }
    }
}
