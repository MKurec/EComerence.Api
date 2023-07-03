using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComerence.Api.Migrations
{
    /// <inheritdoc />
    public partial class CoPucharsedProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocation",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CopurchasedProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopurchasedProductId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ImageLocation",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
