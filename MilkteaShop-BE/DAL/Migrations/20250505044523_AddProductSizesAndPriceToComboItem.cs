using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSizesAndPriceToComboItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_ProductSize_ProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropIndex(
                name: "IX_ComboItems_ProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "ComboItems");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ComboItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ComboItemProductSize",
                columns: table => new
                {
                    ComboItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSizesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItemProductSize", x => new { x.ComboItemsId, x.ProductSizesId });
                    table.ForeignKey(
                        name: "FK_ComboItemProductSize_ComboItems_ComboItemsId",
                        column: x => x.ComboItemsId,
                        principalTable: "ComboItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboItemProductSize_ProductSize_ProductSizesId",
                        column: x => x.ProductSizesId,
                        principalTable: "ProductSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboItemProductSize_ProductSizesId",
                table: "ComboItemProductSize",
                column: "ProductSizesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboItemProductSize");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ComboItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSizeId",
                table: "ComboItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ProductSizeId",
                table: "ComboItems",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_ProductSize_ProductSizeId",
                table: "ComboItems",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
