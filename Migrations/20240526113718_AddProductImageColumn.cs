using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesBack.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
            name: "Image",
            table: "Products",
            type: "bytea",
            nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Image",
            table: "Products");
        }
    }
}
