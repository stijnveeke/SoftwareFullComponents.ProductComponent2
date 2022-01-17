using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareFullComponents.Product2Component.Migrations
{
    public partial class AddedProductGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Product");
        }
    }
}
