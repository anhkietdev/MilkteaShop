using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_ComboItemProductSizes_ComboItemProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_ComboItemProductSizes_ComboItemProductSizeId",
                table: "ProductSize");

            migrationBuilder.DropTable(
                name: "ComboItemProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ProductSize_ComboItemProductSizeId",
                table: "ProductSize");

            migrationBuilder.DropColumn(
                name: "ComboItemProductSizeId",
                table: "ProductSize");

            migrationBuilder.RenameColumn(
                name: "ComboItemProductSizeId",
                table: "ComboItems",
                newName: "ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ComboItems_ComboItemProductSizeId",
                table: "ComboItems",
                newName: "IX_ComboItems_ProductSizeId");

            migrationBuilder.RenameColumn(
                name: "ProductSizesId",
                table: "ComboItemProductSizes",
                newName: "ProductSizeId");

            migrationBuilder.RenameColumn(
                name: "ComboItemsId",
                table: "ComboItemProductSizes",
                newName: "ComboItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItemProductSizes_ComboItemId",
                table: "ComboItemProductSizes",
                column: "ComboItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItemProductSizes_ProductSizeId",
                table: "ComboItemProductSizes",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItemProductSizes_ComboItems_ComboItemId",
                table: "ComboItemProductSizes",
                column: "ComboItemId",
                principalTable: "ComboItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItemProductSizes_ProductSize_ProductSizeId",
                table: "ComboItemProductSizes",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_ProductSize_ProductSizeId",
                table: "ComboItems",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItemProductSizes_ComboItems_ComboItemId",
                table: "ComboItemProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ComboItemProductSizes_ProductSize_ProductSizeId",
                table: "ComboItemProductSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_ProductSize_ProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropIndex(
                name: "IX_ComboItemProductSizes_ComboItemId",
                table: "ComboItemProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ComboItemProductSizes_ProductSizeId",
                table: "ComboItemProductSizes");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "ComboItems",
                newName: "ComboItemProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ComboItems_ProductSizeId",
                table: "ComboItems",
                newName: "IX_ComboItems_ComboItemProductSizeId");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "ComboItemProductSizes",
                newName: "ProductSizesId");

            migrationBuilder.RenameColumn(
                name: "ComboItemId",
                table: "ComboItemProductSizes",
                newName: "ComboItemsId");

            migrationBuilder.AddColumn<Guid>(
                name: "ComboItemProductSizeId",
                table: "ProductSize",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "IX_ProductSize_ComboItemProductSizeId",
                table: "ProductSize",
                column: "ComboItemProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItemProductSize_ProductSizesId",
                table: "ComboItemProductSize",
                column: "ProductSizesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_ComboItemProductSizes_ComboItemProductSizeId",
                table: "ComboItems",
                column: "ComboItemProductSizeId",
                principalTable: "ComboItemProductSizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_ComboItemProductSizes_ComboItemProductSizeId",
                table: "ProductSize",
                column: "ComboItemProductSizeId",
                principalTable: "ComboItemProductSizes",
                principalColumn: "Id");
        }
    }
}
