using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parser.Migrations
{
    /// <inheritdoc />
    public partial class RenameColoumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkCersanit",
                table: "addedDatas",
                newName: "LinkDomotex");

            migrationBuilder.AddColumn<string>(
                name: "LinkDomotex",
                table: "priceLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkVodoparad",
                table: "priceLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "priceLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkDomotex",
                table: "priceLogs");

            migrationBuilder.DropColumn(
                name: "LinkVodoparad",
                table: "priceLogs");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "priceLogs");

            migrationBuilder.RenameColumn(
                name: "LinkDomotex",
                table: "addedDatas",
                newName: "LinkCersanit");
        }
    }
}
