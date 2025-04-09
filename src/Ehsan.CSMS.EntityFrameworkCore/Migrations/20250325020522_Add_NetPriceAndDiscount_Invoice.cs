using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehsan.CSMS.Migrations
{
    /// <inheritdoc />
    public partial class Add_NetPriceAndDiscount_Invoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "Order");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Invoice",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "Invoice",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "Invoice");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
