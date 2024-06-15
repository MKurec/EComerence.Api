using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComerence.Api.Migrations
{
    /// <inheritdoc />
    public partial class ProbablityTab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProductProbabilitys",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Probablity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductProbabilitys", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProductProbabilitys_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProductProbabilitys_Users_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProductProbabilitys");
        }
    }
}
