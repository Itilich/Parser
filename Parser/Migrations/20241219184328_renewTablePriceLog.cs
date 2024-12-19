using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parser.Migrations
{
    /// <inheritdoc />
    public partial class renewTablePriceLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "priceLogs");

            migrationBuilder.AddColumn<double>(
                name: "PriceDomotex",
                table: "priceLogs",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceVodoparad",
                table: "priceLogs",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDomotex",
                table: "priceLogs");

            migrationBuilder.DropColumn(
                name: "PriceVodoparad",
                table: "priceLogs");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "priceLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
