using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehsan.CSMS.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@"
            //    CREATE VIEW vw_OrderDetails
            //    AS
            //    SELECT o.Id, c.CustomerName AS CustomerName, 
            //        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
