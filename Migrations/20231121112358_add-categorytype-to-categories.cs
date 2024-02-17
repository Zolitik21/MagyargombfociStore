using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagyargombfociStore.Migrations
{
    /// <inheritdoc />
    public partial class addcategorytypetocategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Categories",
                newName: "CategoryType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryType",
                table: "Categories",
                newName: "ProductName");

            migrationBuilder.AddColumn<string>(
                name: "Stock",
                table: "Product",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
