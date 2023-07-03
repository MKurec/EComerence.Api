using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComerence.Api.Migrations
{
    /// <inheritdoc />
    public partial class OrderListToDictionary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Orders",
                table: "OrderLists",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orders",
                table: "OrderLists");
        }
    }
}
