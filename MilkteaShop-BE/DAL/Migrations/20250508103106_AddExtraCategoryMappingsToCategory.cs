using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraCategoryMappingsToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CategoryExtraMappings_Categories_CategoryId",
            //    table: "CategoryExtraMappings");

            //migrationBuilder.DropIndex(
            //    name: "IX_CategoryExtraMappings_CategoryId",
            //    table: "CategoryExtraMappings");

            //migrationBuilder.DropColumn(
            //    name: "CategoryId",
            //    table: "CategoryExtraMappings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "CategoryExtraMappings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryExtraMappings_CategoryId",
                table: "CategoryExtraMappings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryExtraMappings_Categories_CategoryId",
                table: "CategoryExtraMappings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
