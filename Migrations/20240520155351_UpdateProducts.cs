using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClothesBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "Images",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateTable(
               name: "Products",
               columns: table => new
               {
                   ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                   ProductName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                   Category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                   Description = table.Column<string>(type: "text", nullable: false),
                   Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                   Price = table.Column<decimal>(type: "numeric", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Products", x => x.ProductId);
               });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Color", "Description", "Price", "ProductName" },
                values: new object[,]
                {
                    { new Guid("3577bed7-82d2-4c81-a877-206e7c1b7fe7"), "Leggings", "Black", "With an increased fabric weight, these leggings ensure a squat-proof finish, empowering you during your workouts. The refined waistband depth provides a true mid-high waist fit, offering essential support.", 48.00m, "Balance V3 Seamless Leggings" },
                    { new Guid("3af24a51-5447-4179-b6e9-bfadeb6b72d3"), "Socks", "Black", "Lifters don't live by Mondays and Tuesdays. We live by Chest Days and Leg Days. Back Days and Rest Days. And these socks know what's up", 40.00m, "GFX Crew Socks 7PK" },
                    { new Guid("5ae3e562-32ab-4094-9b77-461149a8a874"), "Jackets", "Black", "Crafted with enhanced softness and stretch for premium comfort, it offers a new luxurious buttery handfeel.", 45.00m, "Balance V3 Seamless Zip Jacket" },
                    { new Guid("5c9fd67f-68d5-4f55-ac32-acb88094cefe"), "Sweatshirts", "Black", "Soft, brushed back fabric inside for warmth and comfort. Ribbed hem and cuffs for a clean fit. Oversized fit", 50.00m, "Training Oversized Fleece Sweatshirt" },
                    { new Guid("a9a9b493-ce5b-42c4-b965-e553ecb64787"), "Hoodies", "Black", "From rest day relaxing to brunch with the girls, elevate your off-duty vibe in the Phys Ed collection.", 82.00m, "Phys Ed Hoodie" },
                    { new Guid("c99b31e1-e656-4d77-bb04-65ae10869948"), "Tops", "Black", "Re-designed with enhanced softness for a luxurious handfeel and new improved stretch for better comfort, it ensures optimal performance.", 30.00m, "Balance V3 Seamless Crop Top" },
                    { new Guid("d7fd265f-046c-4392-8074-2c3132c27349"), "Shorts", "Black", "Dominate your workout with Balance V3 Seamless Shorts. Crafted with improved softness and stretch, they offer a premium buttery handfeel and a comfortable compressive fit.", 35.00m, "Balance V3 Seamless Shorts" },
                    { new Guid("e1f08108-ab68-4498-a903-dc303185705d"), "T-Shirts", "Black", "Premium heavyweight fabric for comfort that hits different. Physical Education graphic to chest", 30.00m, "Phys Ed Graphic T-Shirt" }
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    ProductVariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Size = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: DateTime.UtcNow)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.ProductVariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3577bed7-82d2-4c81-a877-206e7c1b7fe7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3af24a51-5447-4179-b6e9-bfadeb6b72d3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5ae3e562-32ab-4094-9b77-461149a8a874"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5c9fd67f-68d5-4f55-ac32-acb88094cefe"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a9a9b493-ce5b-42c4-b965-e553ecb64787"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c99b31e1-e656-4d77-bb04-65ae10869948"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d7fd265f-046c-4392-8074-2c3132c27349"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e1f08108-ab68-4498-a903-dc303185705d"));

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Images",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                newName: "IX_Images_ProductVariantId");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductVariants",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductIdd",
                table: "ProductVariants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Description", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "Shorts", "Dominate your workout with Balance V3 Seamless Shorts. Crafted with improved softness and stretch, they offer a premium buttery handfeel and a comfortable compressive fit.", 3500, "Balance V3 Seamless Shorts" },
                    { 2, "Jackets", "Crafted with enhanced softness and stretch for premium comfort, it offers a new luxurious buttery handfeel. ", 4500, "Balance V3 Seamless Zip Jacket" },
                    { 3, "Tops", "Re-designed with enhanced softness for a luxurious handfeel and new improved stretch for better comfort, it ensures optimal performance. ", 3000, "Balance V3 Seamless Crop Top" },
                    { 4, "Leggings", "With an increased fabric weight, these leggings ensure a squat-proof finish, empowering you during your workouts. The refined waistband depth provides a true mid-high waist fit, offering essential support.", 4800, "Balance V3 Seamless Leggings" },
                    { 5, "T-Shirts", "Premium heavyweight fabric for comfort that hits different. Physical Education graphic to chest", 3000, "Phys Ed Graphic T-Shirt" },
                    { 6, "Hoodies", "From rest day relaxing to brunch with the girls, elevate your off-duty vibe in the Phys Ed collection.", 8200, "Phys Ed Hoodie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductIdd",
                table: "ProductVariants",
                column: "ProductIdd");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ProductVariants_ProductVariantId",
                table: "Images",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductIdd",
                table: "ProductVariants",
                column: "ProductIdd",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
