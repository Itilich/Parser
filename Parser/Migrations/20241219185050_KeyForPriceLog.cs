using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parser.Migrations
{
    /// <inheritdoc />
    public partial class KeyForPriceLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "priceLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_priceLogs",
                table: "priceLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_priceLogs",
                table: "priceLogs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "priceLogs");
        }
    }
}
