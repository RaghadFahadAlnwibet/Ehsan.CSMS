using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehsan.CSMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderDetailsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Cashier",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Cashier");
        }
    }
}
