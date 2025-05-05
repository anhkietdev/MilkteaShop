using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddComboItemProductSizeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComboItemProductSizeId",
                table: "ProductSize",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComboItemProductSizeId",
                table: "ComboItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComboItemProductSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSizesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItemProductSizes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_ComboItemProductSizeId",
                table: "ProductSize",
                column: "ComboItemProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ComboItemProductSizeId",
                table: "ComboItems",
                column: "ComboItemProductSizeId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_ComboItemProductSizes_ComboItemProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_ComboItemProductSizes_ComboItemProductSizeId",
                table: "ProductSize");

            migrationBuilder.DropTable(
                name: "ComboItemProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSize_ComboItemProductSizeId",
                table: "ProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ComboItems_ComboItemProductSizeId",
                table: "ComboItems");

            migrationBuilder.DropColumn(
                name: "ComboItemProductSizeId",
                table: "ProductSize");

            migrationBuilder.DropColumn(
                name: "ComboItemProductSizeId",
                table: "ComboItems");
        }
    }
}
