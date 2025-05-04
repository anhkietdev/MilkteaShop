using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderItems_ParentOrderItemId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ParentOrderItemId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ParentOrderItemId",
                table: "OrderItems");

            migrationBuilder.CreateTable(
                name: "OrderItemToppings",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemToppings", x => new { x.OrderItemId, x.ProductSizeId });
                    table.ForeignKey(
                        name: "FK_OrderItemToppings_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemToppings_ProductSize_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemToppings_ProductSizeId",
                table: "OrderItemToppings",
                column: "ProductSizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemToppings");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentOrderItemId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ParentOrderItemId",
                table: "OrderItems",
                column: "ParentOrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderItems_ParentOrderItemId",
                table: "OrderItems",
                column: "ParentOrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
