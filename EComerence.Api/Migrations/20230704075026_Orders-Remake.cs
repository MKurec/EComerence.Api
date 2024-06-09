using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComerence.Api.Migrations
{
    /// <inheritdoc />
    public partial class OrdersRemake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderLists_OrderListId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderListId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Orders",
                newName: "ProducerName");

            migrationBuilder.RenameColumn(
                name: "OrderListId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "BrandTag",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bought",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BrandTag",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bought",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BrandTag",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "OrderListId");

            migrationBuilder.RenameColumn(
                name: "ProducerName",
                table: "Orders",
                newName: "ProductName");

            migrationBuilder.AlterColumn<string>(
                name: "BrandTag",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderListId",
                table: "Orders",
                column: "OrderListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderLists_OrderListId",
                table: "Orders",
                column: "OrderListId",
                principalTable: "OrderLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
