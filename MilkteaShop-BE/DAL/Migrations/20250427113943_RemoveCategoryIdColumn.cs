using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategoryIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First drop the foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryExtraMappings_Categories_CategoryId",
                table: "CategoryExtraMappings");

            // Then drop the index
            migrationBuilder.DropIndex(
                name: "IX_CategoryExtraMappings_CategoryId",
                table: "CategoryExtraMappings");

            // Finally drop the column
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CategoryExtraMappings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
