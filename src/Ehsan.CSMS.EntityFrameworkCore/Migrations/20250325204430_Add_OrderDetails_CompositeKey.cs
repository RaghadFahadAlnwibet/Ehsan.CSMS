using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehsan.CSMS.Migrations
{
    /// <inheritdoc />
    public partial class Add_OrderDetails_CompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "OrderDetail_Date",
                table: "OrderDetail",
                newName: "CreationTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "OrderDetail",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                columns: new[] { "OrderId", "ProductId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "OrderDetail",
                newName: "OrderDetail_Date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDetail_Date",
                table: "OrderDetail",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");
        }
    }
}
