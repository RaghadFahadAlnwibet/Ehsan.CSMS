using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehsan.CSMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderMSSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Cashier");

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "discount",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Cashier",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }
    }
}
