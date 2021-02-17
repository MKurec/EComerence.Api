using Microsoft.EntityFrameworkCore.Migrations;

namespace EComerence.Api.Migrations
{
    public partial class RepairMissspelingBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descryption",
                table: "Products",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Descryption");
        }
    }
}
