using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EComerence.Api.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderLists_OrderListId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Order");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "OrderLists",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderListId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderLists_OrderListId",
                table: "Order",
                column: "OrderListId",
                principalTable: "OrderLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderLists_OrderListId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderLists");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderListId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Order",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderLists_OrderListId",
                table: "Order",
                column: "OrderListId",
                principalTable: "OrderLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
