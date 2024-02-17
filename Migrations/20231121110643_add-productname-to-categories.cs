using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagyargombfociStore.Migrations
{
    /// <inheritdoc />
    public partial class addproductnametocategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Product",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Product",
                newName: "Quality");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "ProductName");

            migrationBuilder.AlterColumn<string>(
                name: "Stock",
                table: "Product",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "Product",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Product",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.AlterColumn<bool>(
                name: "Stock",
                table: "Product",
                type: "bit",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
