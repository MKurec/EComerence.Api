using Microsoft.EntityFrameworkCore.Migrations;

namespace EComerence.Api.Migrations
{
    public partial class ProductDescryption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descryption",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descryption",
                table: "Products");
        }
    }
}
