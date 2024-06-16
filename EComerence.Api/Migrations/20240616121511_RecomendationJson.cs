using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EComerence.Api.Migrations
{
    /// <inheritdoc />
    public partial class RecomendationJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProductProbabilitys_Users_ProductId",
                table: "UserProductProbabilitys");

            migrationBuilder.AddColumn<string>(
                name: "RecomendationsJson",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProductProbabilitys_UserId",
                table: "UserProductProbabilitys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductProbabilitys_Users_UserId",
                table: "UserProductProbabilitys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProductProbabilitys_Users_UserId",
                table: "UserProductProbabilitys");

            migrationBuilder.DropIndex(
                name: "IX_UserProductProbabilitys_UserId",
                table: "UserProductProbabilitys");

            migrationBuilder.DropColumn(
                name: "RecomendationsJson",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductProbabilitys_Users_ProductId",
                table: "UserProductProbabilitys",
                column: "ProductId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
