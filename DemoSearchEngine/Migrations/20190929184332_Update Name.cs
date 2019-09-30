using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoSearchEngine.Migrations
{
    public partial class UpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CastCrews");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CastCrews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CastCrews");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CastCrews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
