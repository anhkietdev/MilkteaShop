using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateProductSizeInOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_Products_ProductId",
                table: "ComboItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductSizeId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ComboItems",
                newName: "ProductSizeId");

            migrationBuilder.RenameIndex(
                name: "IX_ComboItems_ProductId",
                table: "ComboItems",
                newName: "IX_ComboItems_ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_ProductSize_ProductSizeId",
                table: "ComboItems",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductSize_ProductSizeId",
                table: "OrderItems",
                column: "ProductSizeId",
                principalTable: "ProductSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_ProductSize_ProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductSize_ProductSizeId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductSizeId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "ComboItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ComboItems_ProductSizeId",
                table: "ComboItems",
                newName: "IX_ComboItems_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_Products_ProductId",
                table: "ComboItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
